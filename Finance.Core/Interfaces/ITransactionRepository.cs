using Finance.Core.Entities;

namespace Finance.Core.Interfaces;

public interface ITransactionRepository : IGenericRepository<UserTransaction>
{
    Task<IEnumerable<UserTransaction>> GetUserTransactions(int walletId);
    Task<IEnumerable<UserTransaction>> GetExistingTransactions();
}