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

    public async Task<ImageUploadResponse> DeleteImageAsync(string publicId)
    {
        var image = await _imageRepository.GetByPublicIdAsync(publicId);
        
        await _imageRepository.RemoveAsync(image!);
        await _cloudinaryService.DeleteAsync(publicId);

        return new ImageUploadResponse
        {
            PublicId = publicId,
            Url = "Remove"
        };
    }

    public async Task<IReadOnlyList<ImageDto>> GetAllImagesAsync()
    {
        var images = await _imageRepository.GetAllAsync();
        
        return images
            .Select(image => new ImageDto(
                image.Url,
                image.PublicId
            ))
            .ToList();
    }
}