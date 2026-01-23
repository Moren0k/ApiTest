using ApiTest.Application.Features.AuthFeatures.DTOs;

namespace ApiTest.Application.Features.AuthFeatures;

public interface IAuthService
{
    public Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest);
    public Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto registerRequest);
}