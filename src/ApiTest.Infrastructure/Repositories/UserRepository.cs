using ApiTest.Domain.Entities;
using ApiTest.Domain.IRepository;
using ApiTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;

    public UserRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // CRUD
    public async Task AddAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    // SEARCH
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
    
    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FindAsync(id);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetByNameAsync(string name)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name);
    }
}