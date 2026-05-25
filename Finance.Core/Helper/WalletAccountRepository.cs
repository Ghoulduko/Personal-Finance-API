using Finance.Core.Database;
using Finance.Core.Entities;
using Finance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Helper;

public class WalletAccountRepository : GenericRepository<WalletAccount>, IWalletAccountRepository
{
    public WalletAccountRepository(PersonalFinanceDbContext context) : base(context) {}
    
    public async Task<WalletAccount?> GetWalletByUserId(int userId)
    {
        return await _dbSet.Include(w => w.User).SingleOrDefaultAsync(w => w.UserId == userId);
    }
}