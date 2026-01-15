namespace ApiTest.Application.DTOs;

public record LoginRequestDto(
    string Email,
    string Password
);

public record RegisterRequestDto(
    string Name,
    string Email,
    string Password,
    bool IsAdmin
);

public record AuthResponseDto(
    string AccessToken,
    UserDto User
);