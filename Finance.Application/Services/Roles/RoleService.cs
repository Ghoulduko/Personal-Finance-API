using Finance.Application.Dtos.Role;
using Finance.Application.Interfaces;
using Finance.Core.Entities;
using Finance.Core.Exceptions;
using Finance.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace Finance.Application.Services.Roles;

public class RoleService : IRoleService
{
    private readonly IGenericRepository<Role> _repository;
    private readonly ILogger<RoleService> _logger;

    public RoleService(IGenericRepository<Role> repository, ILogger<RoleService> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task AddRoleAsync(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
            throw new InvalidRoleNameException("Role name is required");
        
        await _repository.AddAsync(new Role() {Name = roleName.Trim().ToUpper()});
        _logger.LogInformation("Added Role: {RoleName}", roleName);
    }

    public async Task<IEnumerable<RoleDto>> GetAllRolesAsync()
    {
        var allRoles = await _repository.GetAllAsync();
        return allRoles.Select(r => new RoleDto()
        {
            Id = r.Id,
            Name = r.Name
        });
        
    }

    public async Task<RoleDto> GetRoleByNameAsync(string roleName)
    {
        var role = await _repository.GetSingleOrDefaultAsync(r => r.Name.Equals(roleName.Trim().ToUpper()));
        if (role is null)
            throw new RoleNotFoundException($"No Role was found with name: {roleName.Trim().ToUpper()}");
        return new RoleDto()
        {
            Id = role.Id,
            Name = role.Name
        };
        
    }

    public async Task DeleteRoleAsync(int roleId)
    {
        var role = await _repository.GetSingleOrDefaultAsync(r => r.Id == roleId);
        if (role is null)
            throw new RoleNotFoundException($"No Role was found with the ID: {roleId}");
        await _repository.DeleteAsync(role);
        _logger.LogInformation("Deleted Role: {RoleId}", roleId);
    }
}