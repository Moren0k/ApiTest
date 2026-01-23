namespace ApiTest.Domain.Entities.EUser;

public interface IUserRepository
{
    // WRITE
    public void Add(EUser.User user);
    public void Update(EUser.User user);
    public void Remove(EUser.User user);

    // READ
    public Task<IReadOnlyList<EUser.User>> GetAllAsync();
    public Task<EUser.User?> GetByIdAsync(Guid id);
    public Task<EUser.User?> GetByEmailAsync(string email);
    public Task<EUser.User?> GetByNameAsync(string name);
}