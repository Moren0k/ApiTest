using CloudinaryDotNet.Actions;

namespace ApiTest.Application.IServices;

public interface IStorageImage
{
    public Task<ImageUploadResult> UploadAsync(Stream file, string fileName);
    public Task DeleteAsync(string publicId);
}