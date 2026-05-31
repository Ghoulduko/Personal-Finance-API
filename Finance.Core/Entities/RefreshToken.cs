using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Core.Entities;

[Table("RefreshTokens")]
public class RefreshToken
{
    public int Id { get; set; }
    public string TokenHash { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresOn { get; set; }

    public User User { get; set; }
}