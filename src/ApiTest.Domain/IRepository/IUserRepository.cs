using ApiTest.Domain.Entities;

namespace ApiTest.Domain.IRepository;

public interface IUserRepository
{
    // CRUD
    public Task<IEnumerable<User>> GetAllUsersAsync();
    public Task AddUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task DeleteUserAsync(User user);
    
    // SEARCH
    public Task<User> GetUserByIdAsync(Guid id);
    public Task<User> GetUserByEmailAsync(string email);
    public Task<User> GetUserByUsernameAsync(string name);
}