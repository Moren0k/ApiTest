using ApiTest.Application.DTOs;

namespace ApiTest.Application.IServices;

public interface ICloudinaryService
{
    public Task<ImageUploadResponse> UploadAsync(Stream file, string fileName);
    public Task DeleteAsync(string publicId);
}