namespace ApiTest.Domain.Entities.Image;

public interface IImageRepository
{
    // WRITE
    public void Add(Image image);
    public void Update(Image image);
    public void Remove(Image image);

    // READ
    public Task<IReadOnlyList<Image>> GetAllAsync();
    public Task<Image?> GetByIdAsync(Guid id);
    public Task<Image?> GetByPublicIdAsync(string publicId);
    public Task<Image?> GetByUrlAsync(string url);
}