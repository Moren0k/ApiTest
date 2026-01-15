using ApiTest.Domain.Entities;

namespace ApiTest.Application.IServices;

public interface IUserService
{
    // CRUD
    public Task UpdateUserAsync(Guid id);
    public Task DeleteUserAsync(Guid id);
    
    // SEARCH
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task<User?> GetUserByIdAsync(Guid id);
    public Task<User?> GetUserByEmailAsync(string email);
    public Task<User?> GetUserByNameAsync(string name);
}