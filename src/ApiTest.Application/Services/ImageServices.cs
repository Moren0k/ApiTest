using ApiTest.Application.DTOs;
using ApiTest.Application.IServices;
using ApiTest.Domain.Entities;
using ApiTest.Domain.IRepository;

namespace ApiTest.Application.Services;

public class ImageServices : IImageServices
{
    private readonly IImageRepository _imageRepository;
    private readonly ICloudinaryService _cloudinaryService;
    
    public ImageServices(ICloudinaryService cloudinaryService, IImageRepository imageRepository)
    {
        _cloudinaryService = cloudinaryService;
        _imageRepository = imageRepository;
    }
    
    public async Task<ImageUploadResponse> UploadImageAsync(Stream file, string fileName)
    {
        var result = await _cloudinaryService.UploadAsync(file, fileName);

        var newImg = new Image(result.PublicId, result.Url.ToString());
        
        await _imageRepository.AddAsync(newImg);
        
        return new ImageUploadResponse
        {
            PublicId = result.PublicId,
            Url = result.Url
        };
    }

    public Task<ImageUploadResponse> UpdateImageAsync(Stream file, string fileName)
    {
        throw new NotImplementedException();
    }

    public Task<ImageUploadResponse> DeleteImageAsync(string publicId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<ImageDto>> GetAllImagesAsync()
    {
        throw new NotImplementedException();
    }
}