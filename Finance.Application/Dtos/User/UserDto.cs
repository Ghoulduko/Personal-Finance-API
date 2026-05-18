using Finance.Application.Dtos.Role;

namespace Finance.Application.Dtos.User;

public class UserDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; } 
    public decimal Balance { get; set; }
    public string Role { get; set; }
}