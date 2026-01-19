using ApiTest.Application.DTOs;
using CloudinaryDotNet.Actions;

namespace ApiTest.Application.IServices;

public interface IImageServices
{
    // CRUD
    public Task<ImageUploadResult> UploadImageAsync(Stream file, string fileName);
    public Task<ImageUploadResult> UpdateImageAsync(Stream file, string fileName);
    public Task<ImageUploadResult> DeleteImageAsync(string publicId);
    
    // SEARCH
    public Task<IEnumerable<ImageDto>> GetAllImagesAsync();
}