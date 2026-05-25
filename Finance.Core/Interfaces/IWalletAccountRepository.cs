using Finance.Core.Entities;

namespace Finance.Core.Interfaces;

public interface IWalletAccountRepository : IGenericRepository<WalletAccount>
{
    Task<WalletAccount?> GetWalletByUserId(int userId);
}