using Finance.Application.Dtos.Auth;
using Finance.Application.Interfaces;
using Finance.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Personal_Finance_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IJwtAuthenticationService _jwtAuthenticationService;

    public AccountController(IJwtAuthenticationService jwtAuthenticationService)
    {
        _jwtAuthenticationService = jwtAuthenticationService;
    }

    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto req)
    {
        return Ok(await _jwtAuthenticationService.Login(req));
    }

    [AllowAnonymous]
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto req)
    {
        return Ok(await _jwtAuthenticationService.Register(req));
    }

    [AllowAnonymous]
    [HttpPost("Refresh")]
    public async Task<IActionResult> Refresh(string token)
    {
        return Ok(await _jwtAuthenticationService.RotateRefreshToken(token));
    }
}