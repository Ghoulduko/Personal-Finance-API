using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Finance.Application.Dtos.Auth;
using Finance.Application.Dtos.User;
using Finance.Application.ExtensionMethods;
using Finance.Application.Interfaces;
using Finance.Core.Entities;
using Finance.Core.Exceptions;
using Finance.Core.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Application.Services.Authentication;

public class JwtAuthenticationService : IJwtAuthenticationService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IValidator<RegisterRequestDto> _registerValidator;
    private readonly IValidator<LoginRequestDto> _loginValidator;
    private readonly IEmailNotificationService _emailService;
    private readonly ILogger<JwtAuthenticationService> _logger;

    public JwtAuthenticationService(
        IConfiguration configuration,
        IUserRepository userRepository,
        IRefreshTokenRepository refreshTokenRepository,
        IValidator<RegisterRequestDto> registerValidator,
        IValidator<LoginRequestDto> loginValidator,
        IEmailNotificationService emailService,
        ILogger<JwtAuthenticationService> logger)
    {
        _configuration = configuration;
        _userRepository = userRepository;
        _refreshTokenRepository = refreshTokenRepository;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _emailService = emailService;
        _logger = logger;
    }

    public async Task<LoginResponseDto?> Login(LoginRequestDto req)
    {
        await _loginValidator.ValidateAndThrowAsync(req);

        var user = await _userRepository.GetUserByEmail(req.Email);
        if (user is null || user.IsDeleted || !BC.Verify(req.Password, user.PasswordHash))
            return null;

        try
        {
            await _emailService.SendLoginEmail(req.Email, user.Username);
        }
        catch
        {
            _logger.LogError("Sending email on user login failed.");
        }
        return await GenerateJwtToken(user);
    }

    public async Task<LoginResponseDto?> Register(RegisterRequestDto req)
    {
        await _registerValidator.ValidateAndThrowAsync(req);
        var existingUser = await _userRepository.GetUserByEmail(req.Email);
        if (existingUser is not null)
            return null;

        var newUser = new User
        {
            Username = req.Username,
            Email = req.Email.ToLower().Trim(),
            PasswordHash = BC.HashPassword(req.Password, 6),
            RoleId = 1,
            WalletAccount = new WalletAccount
            {
                Balance = 0,
                CreatedAt = DateTime.UtcNow,
            }
        };

        await _userRepository.AddAsync(newUser);
        
        try
        {
            await _emailService.SendRegisterEmail(req.Email, req.Username);
        }
        catch
        {
            _logger.LogError("Sending email on user register failed.");
        }
        
        var user = await _userRepository.GetUserByEmail(req.Email.ToLower().Trim());
        return await GenerateJwtToken(user);
        
        
    }

    public async Task<LoginResponseDto?> RotateRefreshToken(string token)
    {
        var incomingTokenHashed = token.GenerateTokenHash();
        var databaseToken = await _refreshTokenRepository.GetRefreshToken(incomingTokenHashed);
        if (databaseToken is null)
            return null;

        var user = await _userRepository.GetUserById(databaseToken.UserId);
        if (user is null || user.IsDeleted)
            return null;
        
        await _refreshTokenRepository.Delete(databaseToken);
        return await GenerateJwtToken(user);
    }

    public async Task<LoginResponseDto> GenerateJwtToken(User user)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        var key = Encoding.UTF8.GetBytes(_configuration["JwtConfig:Key"]);
        var tokenValidityMins = int.Parse(_configuration["JwtConfig:JwtTokenValidityMins"]);
        var tokenExpiryTimeStamp = DateTime.UtcNow.AddMinutes(tokenValidityMins);

        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString()),
            new Claim("Username", user.Username),
            new Claim("Email", user.Email),
            new Claim(ClaimTypes.Role, user.Role.Name.ToString()),
        };

        var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            claims: claims,
            expires: tokenExpiryTimeStamp,
            signingCredentials: credentials
        );

        var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

        return new LoginResponseDto
        {
            Username = user.Username,
            AccessToken = accessToken,
            Expires = tokenExpiryTimeStamp,
            RefreshToken = await GenerateRefreshToken(user.Id),
        };
    }

    public async Task<string> GenerateRefreshToken(int userId)
    {
        var refreshTokenValidityMins = int.Parse(_configuration["JwtConfig:RefreshTokenValidityMins"]);

        var bytes = RandomNumberGenerator.GetBytes(64);
        var token = Convert.ToBase64String(bytes);

        var refreshToken = new RefreshToken()
        {
            TokenHash = token.GenerateTokenHash(),
            UserId = userId,
            CreatedOn = DateTime.UtcNow,
            ExpiresOn = DateTime.UtcNow.AddMinutes(refreshTokenValidityMins),
        };

        await _refreshTokenRepository.AddRefreshToken(refreshToken);
        return token;
    }
}