using Finance.Application.Dtos.User;
using Finance.Application.Interfaces;
using Finance.Application.Services.Notifications;
using Finance.Core.Entities;
using Finance.Core.Exceptions.UserExcepTions;
using Finance.Core.Interfaces;
using FluentValidation;

namespace Finance.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IValidator<UserRegisterDto> _registerValidator;
    private readonly IValidator<UserLoginDto> _loginValidator;
    private readonly ITokenService _tokenService;
    private readonly IEmailNotificationService _emailService;

    public UserService(IUserRepository repository, IValidator<UserRegisterDto> registerValidator,
        IValidator<UserLoginDto> loginValidator, ITokenService tokenService,
        IEmailNotificationService emailService)
    {
        _repository = repository;
        _registerValidator = registerValidator;
        _loginValidator = loginValidator;
        _tokenService = tokenService;
        _emailService = emailService;
    }

    public async Task<string> Register(UserRegisterDto req)
    {
        await _registerValidator.ValidateAndThrowAsync(req);
        var existingUser = await _repository.GetSingleOrDefaultAsync(u => u.Email == req.Email);
        if (existingUser != null)
            throw new UserAlreadyRegisteredException("The email is already registered");

        var user = new User()
        {
            Username = req.Username.Trim(),
            Email = req.Email.Trim().ToLower(),
            Password = BC.HashPassword(req.Password, 8),
            RoleId = 1,
            WalletAccount = new WalletAccount()
            {
                Balance = 0,
                CreatedAt = DateTime.Now,
            }
        };

        await _repository.AddAsync(user);
        var userWithRole = await _repository.GetUserByEmail(user.Email);
        return _tokenService.CreateToken(user);
    }

    public async Task<string> Login(UserLoginDto req)
    {
        await _loginValidator.ValidateAndThrowAsync(req);
        var user = await _repository.GetUserByEmail(req.Email);
        if (user == null || user.IsDeleted)
            throw new UserNotFoundException("No account found with the provided email");

        if (!BC.Verify(req.Password, user.Password))
            throw new IncorrectPasswordException("Password is incorrect");


        var token = _tokenService.CreateToken(user);
        await _emailService.SendLoginEmail(user.Email, user.Username);
        return token;
    }

    public async Task<IEnumerable<UserDto>> GetAllUsers()
    {
        var users = await _repository.GetAllUsers();

        return users.Select(u => new UserDto()
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Balance = u.WalletAccount.Balance,
            Role = u.Role.Name
        });
    }

    public async Task<IEnumerable<UserDto>> GetAllDeletedUsers()
    {
        var users = await _repository.GetAllDeletedUsers();

        return users.Select(u => new UserDto()
        {
            Id = u.Id,
            Username = u.Username,
            Email = u.Email,
            Balance = u.WalletAccount.Balance,
            Role = u.Role.Name
        });
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var user = await _repository.GetUserById(id);
        if (user == null)
            throw new UserNotFoundException($"No user found with id {id}");

        return new UserDto()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Balance = user.WalletAccount.Balance,
            Role = user.Role.Name
        };
    }

    public async Task<UserDto> GetUserByEmail(string email)
    {
        var user = await _repository.GetUserByEmail(email.Trim().ToLower());
        if (user == null)
            throw new UserNotFoundException($"No user found with id {email.Trim().ToLower()}");

        return new UserDto()
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email.Trim().ToLower(),
            Balance = user.WalletAccount.Balance,
            Role = user.Role.Name
        };
    }

    public async Task DeleteAccount(string password, int userId)
    {
        var user = await _repository.GetUserById(userId);
        if (user == null)
            throw new UserNotFoundException($"No user found with id {userId}");

        if (!BC.Verify(password, user.Password))
            throw new UserNotAuthorizedException("You do not have permission to delete this account");

        user.IsDeleted = true;
        await _repository.SaveAsync();
    }
}