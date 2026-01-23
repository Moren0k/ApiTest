using ApiTest.Application.Features.ImageFeatures.DTOs;

namespace ApiTest.Application.IProviders.IExternalServices.ICloudinary;

public interface ICloudinaryProvider
{
    public Task<ImageUploadResponse> UploadAsync(Stream file, string fileName);
    public Task DeleteAsync(string publicId);
}