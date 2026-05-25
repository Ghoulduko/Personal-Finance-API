using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Core.Entities;

[Table("RefreshTokens")]
public class RefreshToken
{
    public Guid Id { get; set; }
    public string TokenHash { get; set; }
    public int UserId { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedOnUtc { get; set; }
    public DateTime ExpiresOnUtc { get; set; }

    public User User { get; set; }
}