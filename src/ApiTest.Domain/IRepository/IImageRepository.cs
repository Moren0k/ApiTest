using ApiTest.Domain.Entities;

namespace ApiTest.Domain.IRepository;

public interface IImageRepository
{
    // CRUD
    public Task AddAsync(Image image);
    public Task UpdateAsync(Image image);
    public Task RemoveAsync(Image image);
    
    // SEARCH
    public Task<IReadOnlyList<Image>> GetAllAsync();
    public Task<Image?> GetByIdAsync(Guid id);
    public Task<Image?> GetByPublicIdAsync(string publicId);
    public Task<Image?> GetByUrlAsync(string url);
}