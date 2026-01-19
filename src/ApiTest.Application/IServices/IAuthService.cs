using ApiTest.Application.DTOs;

namespace ApiTest.Application.IServices;

public interface IAuthService
{
    public Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    public Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto registerRequest);
}