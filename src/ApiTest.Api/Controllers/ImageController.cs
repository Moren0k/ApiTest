using ApiTest.Application.IServices;
using Microsoft.AspNetCore.Authorization;
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
    public async Task<IActionResult> Upload([FromForm] IFormFile file)
    {
        if (file is null || file.Length == 0)
            return BadRequest("Archivo inválido");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return BadRequest("Formato no permitido");

        await using var stream = file.OpenReadStream();
        
        var response = await _imageServices.UploadImageAsync(stream, file.FileName);
        
        return Ok(new {
            response.PublicId,
            response.Url
        });
    }
}