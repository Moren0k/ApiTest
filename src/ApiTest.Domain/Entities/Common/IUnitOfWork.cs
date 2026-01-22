namespace ApiTest.Domain.Entities.Common;

public interface IUnitOfWork : IDisposable
{
    public Task<int> SaveChangesAsync();
}