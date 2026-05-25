using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Finance.Core.Enums;

namespace Finance.Core.Entities;

[Table("Transactions")]
public class UserTransaction
{
    [Key]
    public int Id { get; set; }
    
    public int SenderWalletId { get; set; }
    public WalletAccount SenderWallet { get; set; }
    
    public int ReceiverWalletId { get; set; }
    public WalletAccount ReceiverWallet { get; set; }
    
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string Status { get; set; }
}