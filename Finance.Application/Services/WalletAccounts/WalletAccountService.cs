using Finance.Application.Interfaces;
using Finance.Core.Exceptions.WalletAccountExceptions;
using Finance.Core.Interfaces;

namespace Finance.Application.Services.WalletAccounts;

public class WalletAccountService : IWalletAccountService
{
    private readonly IWalletAccountRepository _repository;
    private readonly IEmailNotificationService _emailNotificationService;

    public WalletAccountService(IWalletAccountRepository repository, IEmailNotificationService emailNotificationService)
    {
        _repository = repository;
        _emailNotificationService = emailNotificationService;
    }

    public async Task Deposit(decimal amount , int loggedInUserId)
    {
        var userWallet = await _repository.GetWalletByUserId(loggedInUserId);
        if (userWallet == null)
            throw new WalletAccountNotFoundException("No wallet found for this user.");
        
        if (amount < 1)
            throw new ArgumentException($"Cannot deposit less than 1$ to your account");
        
        userWallet.Balance += amount;
        await _repository.SaveAsync();
        await _emailNotificationService.SendDepositEmail(userWallet.User.Email, userWallet.User.Username, amount);
    }

    public async Task Withdraw(decimal amount , int loggedInUserId)
    {
        var userWallet = await _repository.GetWalletByUserId(loggedInUserId);
        if (userWallet == null)
            throw new WalletAccountNotFoundException("No wallet found for this user.");
        if (userWallet.Balance < amount)
            throw new InsufficientFundsException($"Cannot withdraw ${amount} insufficient funds.");
        userWallet.Balance -= amount;
        await _repository.SaveAsync();
        await _emailNotificationService.SendWithdrawEmail(userWallet.User.Email, userWallet.User.Username, amount);
    }

    public async Task<decimal> CheckBalance(int loggedInUserId)
    {
        var userWallet = await _repository.GetSingleOrDefaultAsync(w => w.UserId == loggedInUserId);
        return userWallet == null ? throw new WalletAccountNotFoundException("No wallet found for this user.") : userWallet.Balance;
    }
}