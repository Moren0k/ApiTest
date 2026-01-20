using ApiTest.Api.Dtos;
using ApiTest.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Api.Controllers;

[ApiController]
[Route("api/images")]
public sealed class ImageController : ControllerBase
{
    private readonly IImageServices _imageServices;

    public ImageController(IImageServices imageServices)
    {
        _imageServices = imageServices;
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

        var response = await _imageServices.UploadImageAsync(stream, request.File.FileName);

        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var images = await _imageServices.GetAllImagesAsync();
        return Ok(images);
    }

    [HttpDelete("{publicId}")]
    public async Task<IActionResult> Delete(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            return BadRequest("PublicId inválido");

        var response = await _imageServices.DeleteImageAsync(publicId);
        return Ok(response);
    }
}