using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Core.Entities;

[Table("Users")]
public class User
{
    [Key] public int Id { get; set; }
    [Required] public required string Username { get; set; }
    [Required] public required string Email { get; set; }
    [Required] public required string PasswordHash { get; set; }

    public int RoleId { get; set; }
    public Role Role { get; set; }

    public WalletAccount WalletAccount { get; set; }

    public bool IsDeleted { get; set; } = false;
}