using ApiTest.Domain.Entities;
using ApiTest.Domain.IRepository;
using ApiTest.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Infrastructure.Repositories;

public class ImageRepository : IImageRepository
{
    private readonly AppDbContext _dbContext;

    public ImageRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    // CRUD
    public async Task AddAsync(Image image)
    {
        _dbContext.Images.Add(image);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Image image)
    {
        _dbContext.Images.Update(image);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(Image image)
    {
        _dbContext.Images.Remove(image);
        await _dbContext.SaveChangesAsync();
    }

    // SEARCH
    public async Task<IEnumerable<Image>> GetAllAsync()
    {
        return await _dbContext.Images.ToListAsync();
    }

    public async Task<Image?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Images.FindAsync(id);
    }

    public async Task<Image?> GetByPublicIdAsync(string publicId)
    {
        return await _dbContext.Images.FirstOrDefaultAsync(i => i.PublicId == publicId);
    }

    public async Task<Image?> GetByUrlAsync(string url)
    {
        return await _dbContext.Images.FirstOrDefaultAsync(i => i.Url == url);
    }
}