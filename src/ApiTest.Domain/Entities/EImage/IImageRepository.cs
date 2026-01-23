namespace ApiTest.Domain.Entities.EImage;

public interface IImageRepository
{
    // WRITE
    public void Add(EImage.Image image);
    public void Update(EImage.Image image);
    public void Remove(EImage.Image image);

    // READ
    public Task<IReadOnlyList<EImage.Image>> GetAllAsync();
    public Task<EImage.Image?> GetByIdAsync(Guid id);
    public Task<EImage.Image?> GetByPublicIdAsync(string publicId);
    public Task<EImage.Image?> GetByUrlAsync(string url);
}