using ApiTest.Application.DTOs;
using ApiTest.Domain.Entities;
using ApiTest.Domain.Enums;

namespace ApiTest.Application.IServices;

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