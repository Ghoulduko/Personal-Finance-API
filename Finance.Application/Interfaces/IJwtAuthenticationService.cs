using Finance.Application.Dtos.Auth;
using Finance.Core.Entities;

namespace Finance.Application.Interfaces;

public interface IJwtAuthenticationService
{
    Task<LoginResponseDto?> Login(LoginRequestDto req);
    Task<LoginResponseDto?> Register(RegisterRequestDto req);
    Task<LoginResponseDto?> RotateRefreshToken(string token);
}