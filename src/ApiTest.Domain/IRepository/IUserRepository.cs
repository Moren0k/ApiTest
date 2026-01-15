using ApiTest.Domain.Entities;

namespace ApiTest.Domain.IRepository;

public interface IUserRepository
{
    // CRUD
    public Task AddAsync(User user);
    public Task UpdateAsync(User user);
    public Task RemoveAsync(User user);
    
    // SEARCH
    public Task<IEnumerable<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByNameAsync(string name);
}