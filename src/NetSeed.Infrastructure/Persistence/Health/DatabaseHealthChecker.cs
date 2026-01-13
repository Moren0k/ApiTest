namespace NetSeed.Infrastructure.Persistence.Health;

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