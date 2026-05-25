namespace Finance.Application.Dtos.User;

public class UserForWalletDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; } 
    public string Role { get; set; }
}