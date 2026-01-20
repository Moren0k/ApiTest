using ApiTest.Application.DTOs;

namespace ApiTest.Application.IServices;

public interface IImageServices
{
    // CRUD
    public Task<ImageUploadResponse> UploadImageAsync(Stream file, string fileName);
    public Task<ImageUploadResponse> UpdateImageAsync(Stream file, string fileName);
    public Task<ImageUploadResponse> DeleteImageAsync(string publicId);
    
    // SEARCH
    public Task<IReadOnlyList<ImageDto>> GetAllImagesAsync();
}