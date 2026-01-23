using ApiTest.Application.Features.ImageFeatures.DTOs;
using ApiTest.Application.IProviders.IExternalServices.ICloudinary;
using ApiTest.Domain.Entities.Common;
using ApiTest.Domain.Entities.EImage;

namespace ApiTest.Application.Features.ImageFeatures;

public class ImageService : IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly ICloudinaryProvider _cloudinaryProvider;
    private readonly IUnitOfWork _unitOfWork;
    
    public ImageService(ICloudinaryProvider cloudinaryProvider, IImageRepository imageRepository, IUnitOfWork unitOfWork)
    {
        _cloudinaryProvider = cloudinaryProvider;
        _imageRepository = imageRepository;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ImageUploadResponse> UploadImageAsync(Stream file, string fileName)
    {
        var result = await _cloudinaryProvider.UploadAsync(file, fileName);

        var newImg = new Image(result.PublicId, result.Url);
        
        _imageRepository.Add(newImg);
        await _unitOfWork.SaveChangesAsync();
        
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
        
        _imageRepository.Remove(image!);
        await _cloudinaryProvider.DeleteAsync(publicId);

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