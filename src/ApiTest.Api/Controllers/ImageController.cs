using ApiTest.Api.Dtos;
using ApiTest.Application.Features.ImageFeatures;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Api.Controllers;

[ApiController]
[Route("api/images")]
public sealed class ImageController : ControllerBase
{
    private readonly IImageService _imageService;

    public ImageController(IImageService imageService)
    {
        _imageService = imageService;
    }

    [HttpPost("upload")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Upload([FromForm] ImageUploadRequest request)
    {
        if (request.File is null || request.File.Length == 0)
            return BadRequest("Archivo inválido");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return BadRequest("Formato no permitido");

        await using var stream = request.File.OpenReadStream();

        var response = await _imageService.UploadImageAsync(stream, request.File.FileName);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var images = await _imageService.GetAllImagesAsync();
        return Ok(images);
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> Delete(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return BadRequest("PublicId inválido");

        var response = await _imageService.DeleteImageAsync(publicId);
        return Ok(response);
    }
}