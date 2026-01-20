using ApiTest.Application.DTOs;
using ApiTest.Application.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace ApiTest.Infrastructure.Services;

public class CloudinaryService : ICloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        
        _cloudinary = new Cloudinary(account);
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
            throw new InvalidOperationException(result.Error.Message);

        return new ImageUploadResponse
        {
            PublicId = result.PublicId,
            Url = result.SecureUrl.AbsoluteUri
        };
    }

    public async Task DeleteAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deleteParams);
    }
}