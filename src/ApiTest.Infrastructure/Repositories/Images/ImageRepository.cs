using ApiTest.Domain.Entities.Image;
using ApiTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Infrastructure.Repositories.Images;

public class ImageRepository : IImageRepository
{
    private readonly AppDbContext _dbContext;

    public ImageRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // WRITE
    public void Add(Image image)
    {
        _dbContext.Images.Add(image);
    }

    public void Update(Image image)
    {
        _dbContext.Images.Update(image);
    }

    public void Remove(Image image)
    {
        _dbContext.Images.Remove(image);
    }

    // READ
    public async Task<IReadOnlyList<Image>> GetAllAsync()
    {
        return await _dbContext.Images.AsNoTracking().ToListAsync();
    }

    public async Task<Image?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Images.AsNoTracking().FirstOrDefaultAsync(i => i.Id == id);
    }

    public async Task<Image?> GetByPublicIdAsync(string publicId)
    {
        return await _dbContext.Images.AsNoTracking().FirstOrDefaultAsync(i => i.PublicId == publicId);
    }

    public async Task<Image?> GetByUrlAsync(string url)
    {
        return await _dbContext.Images.AsNoTracking().FirstOrDefaultAsync(i => i.Url == url);
    }
}