using Finance.Application.Dtos.Role;
using Finance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : Controller
{
    private readonly IRoleService _service;

    public RoleController(IRoleService service)
    {
        _service = service;
    }

    [HttpPost("AddRole")]
    [Authorize(Roles = "SUPERADMIN,OWNER")]
    public async Task<Ok<string>> AddRole(string roleName)
    {
        await _service.AddRoleAsync(roleName);
        return TypedResults.Ok($"Successfully added role {roleName.Trim().ToUpper()}");
    }
    
    [HttpGet("GetAllRoles")]
    [Authorize(Roles = "ADMIN,SUPERADMIN,OWNER")]
    public async Task<Ok<IEnumerable<RoleDto>>> GetAllRoles()
    {
        return TypedResults.Ok(await _service.GetAllRolesAsync());
    }

    [HttpGet("GetRoleByName/{roleName}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN,OWNER")]
    public async Task<Ok<RoleDto>> GetRoleByName(string roleName)
    {
        return TypedResults.Ok(await _service.GetRoleByNameAsync(roleName));
    }

    [HttpDelete("DeleteRoleById/{roleId:int}")]
    [Authorize(Roles = "SUPERADMIN,OWNER")]
    public async Task<Ok<string>> DeleteRole(int roleId)
    {
        await _service.DeleteRoleAsync(roleId);
        return TypedResults.Ok($"Role was successfully deleted");
    }
}