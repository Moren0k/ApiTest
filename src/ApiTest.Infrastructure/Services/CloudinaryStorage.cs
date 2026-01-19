using ApiTest.Application.IServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace ApiTest.Infrastructure.Services;

public class CloudinaryStorage : IStorageImage
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryStorage(IOptions<CloudinarySettings> options)
    {
        var settings = options.Value;

        var account = new Account(settings.CloudName, settings.ApiKey, settings.ApiSecret);
        
        _cloudinary = new Cloudinary(account);
    }

    public async Task<ImageUploadResult> UploadAsync(Stream file, string fileName)
    {
        var uploadParams = new ImageUploadParams
        {
            File = new FileDescription(fileName, file),
            Folder = "images",
            UseFilename = false,
            UniqueFilename = true,
            Overwrite = false
        };

        return await _cloudinary.UploadAsync(uploadParams);
    }

    public async Task DeleteAsync(string publicId)
    {
        var deleteParams = new DeletionParams(publicId);
        await _cloudinary.DestroyAsync(deleteParams);
    }
}