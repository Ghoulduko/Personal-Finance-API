using Finance.Core.Database;
using Finance.Core.Entities;
using Finance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Helper;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    public UserRepository(PersonalFinanceDbContext context) : base(context) {}

    private IQueryable<User> BaseQuery()
    {
        return _dbSet.Include(u => u.Role).Include(u => u.WalletAccount);
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
        return await BaseQuery().Where(u => !u.IsDeleted).ToListAsync();
    }

    public async Task<IEnumerable<User>> GetAllDeletedUsers()
    {
        return await BaseQuery().Where(u => u.IsDeleted).ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
        return await BaseQuery().FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        return await BaseQuery().FirstOrDefaultAsync(u => u.Email.Equals(email.Trim()));
    }
}