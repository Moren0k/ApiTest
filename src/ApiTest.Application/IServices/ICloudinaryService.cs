using CloudinaryDotNet.Actions;

namespace ApiTest.Application.IServices;

public interface ICloudinaryService
{
    public Task<ImageUploadResult> UploadAsync(Stream file, string fileName);
    public Task DeleteAsync(string publicId);
}