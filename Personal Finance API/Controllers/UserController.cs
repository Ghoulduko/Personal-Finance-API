using Finance.Application.Dtos.User;
using Finance.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller
{
    private readonly IUserService _service;
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpPost("Register")]
    public async Task<Ok<string>> Register([FromBody] UserRegisterDto req)
    {
        var token = await _service.Register(req);
        return TypedResults.Ok(token);
    }

    [HttpPost("Login")]
    public async Task<Ok<string>> Login([FromBody] UserLoginDto req)
    {
        return TypedResults.Ok(await _service.Login(req));
    }

    [HttpGet("GetProfile")]
    [Authorize]
    public async Task<Ok<UserDto>> GetProfile()
    {
        var userId = User.FindFirst("UserId")?.Value;
        return TypedResults.Ok(await _service.GetUserById(int.Parse(userId)));
    }

    [HttpGet("GetAllUsers")]
    // [Authorize(Roles = "ADMIN,SUPERADMIN,OWNER")]
    public async Task<Ok<IEnumerable<UserDto>>> GetAllUsers()
    {
        return TypedResults.Ok(await _service.GetAllUsers());
    }
    
    [HttpGet("GetAllDeletedUsers")]
    // [Authorize(Roles = "ADMIN,SUPERADMIN,OWNER")]
    public async Task<Ok<IEnumerable<UserDto>>> GetAllDeletedUsers()
    {
        return TypedResults.Ok(await _service.GetAllDeletedUsers());
    }

    [HttpGet("GetUserById/{id:int}")]
    [Authorize(Roles = "ADMIN,SUPERADMIN,OWNER")]
    public async Task<Ok<UserDto>> GetUserById(int id)
    {
        return TypedResults.Ok(await _service.GetUserById(id));
    }

    [HttpGet("GetUserByEmail/{email}")]
    [Authorize]
    public async Task<Ok<UserDto>> GetUserByEmail(string email)
    {
        return TypedResults.Ok(await _service.GetUserByEmail(email));
    }

    [HttpDelete("DeleteAccount")]
    [Authorize]
    public async Task<Ok<string>> DeleteAccount(string password)
    {
        var userId = User.FindFirst("UserId")?.Value;
        await _service.DeleteAccount(password, int.Parse(userId));
        return TypedResults.Ok("Account deleted successfully!");
    }
}