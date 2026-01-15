using ApiTest.Domain.Enums;

namespace ApiTest.Application.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string Role
);

public record ChangeUserRoleRequest(
    UserRole Role
);