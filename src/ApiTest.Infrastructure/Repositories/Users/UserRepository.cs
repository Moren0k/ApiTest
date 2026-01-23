using ApiTest.Domain.Entities.EUser;
using ApiTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Infrastructure.Repositories.Users;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // WRITE
    public void Add(User user)
    {
        _dbContext.Users.Add(user);
    }

    public void Update(User user)
    {
        _dbContext.Users.Update(user);
    }

    public void Remove(User user)
    {
        _dbContext.Users.Remove(user);
    }

    // READ
    public async Task<IReadOnlyList<User>> GetAllAsync()
    {
        return await _dbContext.Users.AsNoTracking().ToListAsync();
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(u => u.ProfileImage)
            .Include(u => u.Gallery)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        return await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Name == name);
    }
}