using ApiTest.Domain.Entities.EUser;

namespace ApiTest.Application.Features.UserFeatures.DTOs;

public record UserDto(
    Guid Id,
    string Name,
    string Email,
    string Role
);

public record ChangeUserRoleRequest(
    UserRole Role
);