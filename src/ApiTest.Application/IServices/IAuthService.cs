using ApiTest.Application.DTOs;

namespace ApiTest.Application.IServices;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto registerRequest);
}