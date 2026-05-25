using Finance.Application.Dtos.User;
using Finance.Application.Dtos.WalletAccount;

namespace Finance.Application.Dtos.Transaction;

public class TransactionDto
{
    public int Id { get; set; }
    
    public string SenderEmail { get; set; }
    public string ReceiverEmail { get; set; }
    
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; }
}