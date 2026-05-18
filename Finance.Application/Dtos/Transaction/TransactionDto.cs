using Finance.Application.Dtos.WalletAccount;

namespace Finance.Application.Dtos.Transaction;

public class TransactionDto
{
    public int Id { get; set; }
    
    public int SenderWalletId { get; set; }
    public WalletAccountDto SenderWallet { get; set; }
    
    public int ReceiverWalletId { get; set; }
    public WalletAccountDto ReceiverWallet { get; set; }
    
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; }
}