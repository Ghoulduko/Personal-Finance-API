using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Finance.Application.Interfaces;
using Finance.Core.Entities;
using Finance.Core.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Finance.Application.Services.Authentication;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(User user)
    {
        var claims = new List<Claim>()
        {
            new Claim("UserId", user.Id.ToString()),
            new Claim("UserEmail", user.Email),
            new Claim("SignInTime", DateTime.UtcNow.ToString()),
            new Claim(ClaimTypes.Role, user.Role.Name),
        };
        
        var secretKey = _configuration["JwtSecretKey"] ?? throw new JwtKeyNotFoundException("No secret key found.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
        
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "https://amazon.com/",
            audience: "https://amazon.com/",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: credentials
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}