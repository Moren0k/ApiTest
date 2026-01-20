namespace ApiTest.Application.DTOs;

public sealed class ImageUploadResponse
{
    public string PublicId { get; init; } = null!;
    public string Url { get; init; } = null!;
}
