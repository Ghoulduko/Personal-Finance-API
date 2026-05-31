using Finance.Application.Dtos.User;
using Finance.Application.Interfaces;
using Finance.Core.Exceptions.UserExcepTions;
using Finance.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Finance.Application.Services.Users;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly IEmailNotificationService _emailService;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository repository,
        IEmailNotificationService emailService,
        ILogger<UserService> logger)
    {
        _repository = repository;
        _emailService = emailService;
        _logger = logger;
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
        if (user is null)
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
        if (user is null)
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
        if (user is null)
            throw new UserNotFoundException($"No user found with id {userId}");

        if (!BC.Verify(password, user.PasswordHash))
            throw new UserNotAuthorizedException("You do not have permission to delete this account");

        user.IsDeleted = true;
        await _repository.SaveAsync();
        
        _logger.LogInformation("Deleted Account: {UserId}", userId);
        
        try
        {
            await _emailService.SendAccountDeletedEmail(user.Email, user.Username);
        }
        catch
        {
            _logger.LogInformation("Failed to delete account: {UserId}", userId);
        }
    }
}