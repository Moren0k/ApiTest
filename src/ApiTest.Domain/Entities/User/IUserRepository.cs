namespace ApiTest.Domain.Entities.User;

public interface IUserRepository
{
    // WRITE
    public void Add(User user);
    public void Update(User user);
    public void Remove(User user);

    // READ
    public Task<IReadOnlyList<User>> GetAllAsync();
    public Task<User?> GetByIdAsync(Guid id);
    public Task<User?> GetByEmailAsync(string email);
    public Task<User?> GetByNameAsync(string name);
}