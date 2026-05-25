using Finance.Core.Database;
using Finance.Core.Entities;
using Finance.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Finance.Core.Helper;

public class TransactionRepository : GenericRepository<UserTransaction>, ITransactionRepository
{
    public TransactionRepository(PersonalFinanceDbContext context) : base(context) {}

    private IQueryable<UserTransaction> BaseQuery()
    {
        return _dbSet
            .Include(t => t.SenderWallet)
            .ThenInclude(w => w.User)
            .Include(t => t.ReceiverWallet)
            .ThenInclude(w => w.User);
    }

    public async Task<IEnumerable<UserTransaction>> GetUserTransactions(int walletId)
    {
        return await BaseQuery().Where(t => t.SenderWalletId == walletId || t.ReceiverWalletId == walletId).ToListAsync();
    }

    public async Task<IEnumerable<UserTransaction>> GetExistingTransactions()
    {
        return await BaseQuery().ToListAsync();
    }
}