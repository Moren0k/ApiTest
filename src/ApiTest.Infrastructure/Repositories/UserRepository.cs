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

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }

    public async Task AddUserAsync(User user)
    {
        _dbContext.Users.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<User> GetUserByIdAsync(Guid id)
    {
        User user = await _dbContext.Users.FindAsync(id)
                    ?? throw new InvalidOperationException();
        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Email == email)
                    ?? throw new InvalidOperationException();
        return user;
    }

    public async Task<User> GetUserByUsernameAsync(string name)
    {
        User user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Name == name)
                    ?? throw new InvalidOperationException();
        return user;
    }
}