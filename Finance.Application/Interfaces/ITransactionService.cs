using Finance.Application.Dtos.Transaction;

namespace Finance.Application.Interfaces;

public interface ITransactionService
{
    Task TransferMoney(string senderEmail, TransferMoneyDto req);
    Task<IEnumerable<TransactionDto>> GetUserTransactions(int loggedInUserId);
    Task<IEnumerable<TransactionDto>> GetExistingTransactions();
}