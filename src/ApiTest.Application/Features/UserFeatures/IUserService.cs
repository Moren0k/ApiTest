using ApiTest.Application.Features.UserFeatures.DTOs;
using ApiTest.Domain.Entities.EUser;

namespace ApiTest.Application.Features.UserFeatures;

public interface IUserService
{
    // CRUD
    public Task UpdateUserAsync(Guid id);
    public Task DeleteUserAsync(Guid id);
    
    // SEARCH
    public Task<IEnumerable<UserDto>> GetAllUsersAsync();
    public Task<UserDto?> GetUserByIdAsync(Guid id);
    public Task<UserDto?> GetUserByEmailAsync(string email);
    public Task<UserDto?> GetUserByNameAsync(string name);
    
    public Task<UserDto> ChangeUserRoleAsync(Guid userId, UserRole role);
}