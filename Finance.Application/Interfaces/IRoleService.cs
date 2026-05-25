using Finance.Application.Dtos.Role;
using Finance.Core.Entities;

namespace Finance.Application.Interfaces;

public interface IRoleService
{
    Task AddRoleAsync(string roleName);
    Task<IEnumerable<RoleDto>> GetAllRolesAsync();
    Task<RoleDto> GetRoleByNameAsync(string roleName);
    Task DeleteRoleAsync(int roleId);
}