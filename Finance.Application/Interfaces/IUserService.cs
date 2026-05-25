using Finance.Application.Dtos.User;

namespace Finance.Application.Interfaces;

public interface IUserService
{
    Task<string> Register(UserRegisterDto req);
    Task<string> Login(UserLoginDto req);
    Task<IEnumerable<UserDto>> GetAllUsers();
    Task<IEnumerable<UserDto>> GetAllDeletedUsers();
    Task<UserDto> GetUserById(int id);
    Task<UserDto> GetUserByEmail(string email);
    Task DeleteAccount(string password, int userId);
}