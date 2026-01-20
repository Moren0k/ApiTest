namespace ApiTest.Api.Dtos;

public sealed class ImageUploadRequest
{
    public IFormFile File { get; init; } = default!;
}