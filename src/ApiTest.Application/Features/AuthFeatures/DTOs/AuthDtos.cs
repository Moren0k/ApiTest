using ApiTest.Application.Features.UserFeatures.DTOs;
using ApiTest.Domain.Entities.EUser;

namespace ApiTest.Application.Features.AuthFeatures.DTOs;

public record LoginRequestDto(
    string Email,
    string Password
);

public record RegisterRequestDto(
    string Name,
    string Email,
    string Password,
    UserRole Role
);

public record AuthResponseDto(
    string AccessToken,
    UserDto User
);