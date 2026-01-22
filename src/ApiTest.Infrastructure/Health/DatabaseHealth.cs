using ApiTest.Infrastructure.Persistence;

namespace ApiTest.Infrastructure.Health;

public interface IDatabaseHealth
{
    Task<HealthResult> HealthAsync();
}

public sealed record HealthResult(
    bool IsHealthy,
    string Dependency,
    string? Provider = null,
    string? Message = null
);

public sealed class DatabaseHealth : IDatabaseHealth
{
    private readonly AppDbContext _dbContext;

    public DatabaseHealth(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<HealthResult> HealthAsync()
    {
        try
        {
            var canConnect = await _dbContext.Database
                .CanConnectAsync();

            return new HealthResult(
                IsHealthy: canConnect,
                Dependency: "Database",
                Provider: _dbContext.Database.ProviderName,
                Message: "Connected"
            );
        }
        catch (Exception ex)
        {
            return new HealthResult(
                IsHealthy: false,
                Dependency: "Database",
                Provider: _dbContext.Database.ProviderName,
                Message: ex.Message
            );
        }
    }
}