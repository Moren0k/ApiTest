using ApiTest.Infrastructure.Persistence;

namespace ApiTest.Infrastructure.Providers.HealthChecks;

public interface ICheckDatabase
{
    Task<CheckResult> Check();
}

public sealed record CheckResult(
    bool IsHealthy,
    string Dependency,
    string? Provider = null,
    string? Message = null
);

public sealed class CheckDatabase : ICheckDatabase
{
    private readonly AppDbContext _dbContext;

    public CheckDatabase(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CheckResult> Check()
    {
        try
        {
            var canConnect = await _dbContext.Database.CanConnectAsync();

            return new CheckResult(
                IsHealthy: canConnect,
                Dependency: "Database",
                Provider: _dbContext.Database.ProviderName,
                Message: "Connected"
            );
        }
        catch (Exception ex)
        {
            return new CheckResult(
                IsHealthy: false,
                Dependency: "Database",
                Provider: _dbContext.Database.ProviderName,
                Message: ex.Message
            );
        }
    }
}