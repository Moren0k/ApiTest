using ApiTest.Application.Features.ImageFeatures.DTOs;

namespace ApiTest.Application.Features.ImageFeatures;

public interface IImageService
{
    // CRUD
    public Task<ImageUploadResponse> UploadImageAsync(Stream file, string fileName);
    public Task<ImageUploadResponse> UpdateImageAsync(Stream file, string fileName);
    public Task<ImageUploadResponse> DeleteImageAsync(string publicId);
    
    // SEARCH
    public Task<IReadOnlyList<ImageDto>> GetAllImagesAsync();
}