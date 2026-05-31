using Finance.Application.Dtos.Transaction;
using Finance.Application.Interfaces;
using Finance.Core.Entities;
using Finance.Core.Enums;
using Finance.Core.Exceptions.TransactionExceptions;
using Finance.Core.Exceptions.UserExcepTions;
using Finance.Core.Exceptions.WalletAccountExceptions;
using Finance.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Finance.Application.Services.Transactions;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IEmailNotificationService _emailNotificationService;
    private readonly ILogger<TransactionService> _logger;

    public TransactionService(ITransactionRepository transactionRepository, IUserRepository userRepository, IEmailNotificationService emailNotificationService, ILogger<TransactionService> logger)
    {
        _transactionRepository = transactionRepository;
        _userRepository = userRepository;
        _emailNotificationService = emailNotificationService;
        _logger = logger;
    }

    private void ValidateTransaction(User sender, decimal amount)
    {
        if (amount <= 0)
            throw new ArgumentException("Amount must be greater than zero.");
        if (sender.WalletAccount.Balance < amount)
            throw new InsufficientFundsException("Insufficient funds.");
    }

    public async Task TransferMoney(string senderEmail, TransferMoneyDto req)
    {
        var sender = await _userRepository.GetUserByEmail(senderEmail);
        var receiver = await _userRepository.GetUserByEmail(req.ReceiverEmail);
        
        if (sender is null || receiver is null || sender.IsDeleted || receiver.IsDeleted)
            throw new UserNotFoundException("User not found, try logging in.");
        
        ValidateTransaction(sender, req.Amount);
        var senderBalanceBeforeTransaction = sender.WalletAccount.Balance;
        var receiverBalanceBeforeTransaction = receiver.WalletAccount.Balance;
        
        sender.WalletAccount.Balance -= req.Amount;
        receiver.WalletAccount.Balance += req.Amount;

        var transaction = new UserTransaction()
        {
            SenderWalletId = sender.WalletAccount.Id,
            SenderWallet = sender.WalletAccount,
            ReceiverWalletId = receiver.WalletAccount.Id,
            ReceiverWallet = receiver.WalletAccount,
            Amount = req.Amount,
            CreatedAt = DateTime.UtcNow,
            Status = TransactionStatus.Pending.ToString()
        };
        
        await _transactionRepository.AddAsync(transaction);

        await Task.Delay(TimeSpan.FromSeconds(5));
        
        if (receiverBalanceBeforeTransaction + req.Amount == receiver.WalletAccount.Balance &&
            senderBalanceBeforeTransaction - req.Amount == sender.WalletAccount.Balance)
        {
            transaction.Status = TransactionStatus.Completed.ToString();
            await _transactionRepository.SaveAsync();
        }
        else
        {
            transaction.Status = TransactionStatus.Failed.ToString();
            await _transactionRepository.SaveAsync();
            throw new TransactionFailedException("Transaction failed.");
        }

        await _emailNotificationService.SendMoneyTransferredEmail(senderEmail, sender.Username, receiver.Username, req.Amount);
        
        await _emailNotificationService.SendReceiveTransferredEmail(req.ReceiverEmail, receiver.Username, sender.Username, req.Amount);
        
        _logger.LogInformation("Money Transferred SenderWalletId: {SenderWalletId}, ReceiverWalletId: {ReceiverWalletId}", sender.WalletAccount.Id, receiver.WalletAccount.Id);
    }

    public async Task<IEnumerable<TransactionDto>> GetUserTransactions(int loggedInUserId)
    {
        var user = await _userRepository.GetUserById(loggedInUserId);
        if (user is null)
            throw new UserNotFoundException("User not found, try logging in.");
        var transactions = await _transactionRepository.GetUserTransactions(user.WalletAccount.Id);
        var transactionDtos = transactions.Select(t => new TransactionDto
        {
            Id = t.Id,
            SenderEmail = t.SenderWallet.User.Email,
            ReceiverEmail = t.ReceiverWallet.User.Email,
            Amount = t.Amount,
            CreatedAt = t.CreatedAt,
            Status = t.Status.ToString()
        });
        return transactionDtos;
    }

    public async Task<IEnumerable<TransactionDto>> GetExistingTransactions()
    {
        var transactions = await _transactionRepository.GetExistingTransactions();
        var transactionDtos = transactions.Select(t => new TransactionDto
        {
            Id = t.Id,
            SenderEmail = t.SenderWallet.User.Email,
            ReceiverEmail = t.ReceiverWallet.User.Email,
            Amount = t.Amount,
            CreatedAt = t.CreatedAt,
            Status = t.Status.ToString()
        });
        return transactionDtos;
    }
}