using Finance.Core.Entities;

namespace Finance.Core.Interfaces;

public interface IUserRepository : IGenericRepository<User>
{
    Task<IEnumerable<User>> GetAllUsers();
    Task<IEnumerable<User>> GetAllDeletedUsers();
    Task<User?> GetUserById(int id);
    Task<User?> GetUserByEmail(string email);
}