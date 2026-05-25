using Finance.Application.Dtos.WalletAccount;

namespace Finance.Application.Interfaces;

public interface IWalletAccountService
{
    Task Deposit(decimal amount , int loggedInUserId);
    Task Withdraw(decimal amount , int loggedInUserId);
    Task<decimal> CheckBalance(int loggedInUserId);
}