using Finance.Application.Dtos.User;

namespace Finance.Application.Dtos.WalletAccount;

public class WalletAccountDto
{
    public int Id { get; set; }
    public UserForWalletDto User { get; set; }
    public decimal Balance { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsDeleted { get; set; }
}