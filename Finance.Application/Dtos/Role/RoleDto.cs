using Finance.Application.Dtos.User;

namespace Finance.Application.Dtos.Role;

public class RoleDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<UserDto> Users { get; set; } = new List<UserDto>();
}