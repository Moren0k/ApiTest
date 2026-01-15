using ApiTest.Infrastructure.Persistence;

namespace ApiTest.Infrastructure.Security;

public class DatabaseHealthChecker
{
    private readonly AppDbContext _dbContext;

    public DatabaseHealthChecker(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> CanConnectAsync()
    {
        return await _dbContext.Database.CanConnectAsync();
    }
}