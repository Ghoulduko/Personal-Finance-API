using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Core.Entities;

[Table("WalletAccounts")]
public class WalletAccount
{
    [Key]
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    public decimal Balance { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsDeleted { get; set; }
}