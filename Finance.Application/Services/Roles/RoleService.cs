using Finance.Application.Dtos.Role;
using Finance.Application.Interfaces;
using Finance.Core.Entities;
using Finance.Core.Exceptions;
using Finance.Core.Interfaces;

namespace Finance.Application.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IGenericRepository<Role> _repository;

    public RoleService(IGenericRepository<Role> repository)
    {
        _repository = repository;
    }
    
    public async Task AddRoleAsync(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
            throw new InvalidRoleNameException("Role name is required");
        
        await _repository.AddAsync(new Role() {Name = roleName.Trim().ToUpper()});
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var allRoles = await _repository.GetAllAsync();
        var allRolesDto = allRoles.Select(r => new RoleDto()
        {
            Id = r.Id,
            Name = r.Name
        });
        return allRolesDto;
    }

    public async Task<RoleDto> GetRoleByNameAsync(string roleName)
    {
        var role = await _repository.GetSingleOrDefaultAsync(r => r.Name.Equals(roleName.Trim().ToUpper()));
        if (role == null)
            throw new RoleNotFoundException($"No Role was found with name: {roleName.Trim().ToUpper()}");
        var roleDto = new RoleDto()
        {
            Id = role.Id,
            Name = role.Name
        };
        return roleDto;
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        var role = await _repository.GetSingleOrDefaultAsync(r => r.Id == roleId);
        if (role == null)
            throw new RoleNotFoundException($"No Role was found with the ID: {roleId}");
        await _repository.DeleteAsync(role);
    }
}