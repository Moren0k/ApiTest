using ApiTest.Application.Features.ImageFeatures.DTOs;
using ApiTest.Application.IProviders.IExternalServices.ICloudinary;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace ApiTest.Infrastructure.Providers.ExternalServices.Cloudinary;

public class CloudinaryProvider : ICloudinaryProvider
{
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryProvider(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        
        _cloudinary = new CloudinaryDotNet.Cloudinary(account);
    }

    public async Task<ImageUploadResponse> UploadAsync(Stream file, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, file),
            Folder = "images",
            UseFilename = false,
            UniqueFilename = true,
            Overwrite = false
        };

        var result = await _cloudinary.UploadAsync(uploadParams);
        
        if (result.Error != null)
        {
            throw new InvalidOperationException(
                $"Cloudinary upload failed: {result.Error.Message}");
        }

        return new ImageUploadResponse
        {
            PublicId = result.PublicId,
            Url = result.SecureUrl.AbsoluteUri
        };
    }

    public async Task DeleteAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        
        var result = await _cloudinary.DestroyAsync(deleteParams);

        if (result.Result != "ok")
        {
            throw new InvalidOperationException(
                $"Cloudinary delete failed: {result.Result}");
        }

    }
}