using Finance.Application.Interfaces;
using Finance.Core.Exceptions.WalletAccountExceptions;
using Finance.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Finance.Application.Services.WalletAccounts;

public class WalletAccountService : IWalletAccountService
{
    private readonly IWalletAccountRepository _repository;
    private readonly IEmailNotificationService _emailNotificationService;
    private readonly ILogger<WalletAccountService> _logger;

    public WalletAccountService(IWalletAccountRepository repository, IEmailNotificationService emailNotificationService, ILogger<WalletAccountService> logger)
    {
        _repository = repository;
        _emailNotificationService = emailNotificationService;
        _logger = logger;
    }

    public async Task Deposit(decimal amount , int loggedInUserId)
    {
        var userWallet = await _repository.GetWalletByUserId(loggedInUserId);
        if (userWallet is null)
            throw new WalletAccountNotFoundException("No wallet found for this user.");
        
        if (amount < 1)
            throw new ArgumentException($"Cannot deposit less than 1$ to your account");
        
        userWallet.Balance += amount;
        await _repository.SaveAsync();
        _logger.LogInformation("Deposited Account: {UserId}", loggedInUserId);
        try
        {
            await _emailNotificationService.SendDepositEmail(userWallet.User.Email, userWallet.User.Username, amount);
        }
        catch
        {
            _logger.LogError("Sending email on deposit failed.");
        }
    }

    public async Task Withdraw(decimal amount , int loggedInUserId)
    {
        var userWallet = await _repository.GetWalletByUserId(loggedInUserId);
        if (userWallet is null)
            throw new WalletAccountNotFoundException("No wallet found for this user.");
        if (userWallet.Balance < amount)
            throw new InsufficientFundsException($"Cannot withdraw ${amount} insufficient funds.");
        userWallet.Balance -= amount;
        await _repository.SaveAsync();
        _logger.LogInformation("Withdrawal of {amount} from User with id: {UserId}", amount, loggedInUserId);
        
        try
        {
            await _emailNotificationService.SendWithdrawEmail(userWallet.User.Email, userWallet.User.Username, amount);
        }
        catch
        {
            _logger.LogError("Sending email on withdrawal failed.");
        }
    }

    public async Task<decimal> CheckBalance(int loggedInUserId)
    {
        var userWallet = await _repository.GetSingleOrDefaultAsync(w => w.UserId == loggedInUserId);
        return userWallet is null ? throw new WalletAccountNotFoundException("No wallet found for this user.") : userWallet.Balance;
    }
}