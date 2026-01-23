namespace ApiTest.Application.Features.ImageFeatures.DTOs;

public sealed class ImageUploadResponse
{
    public string PublicId { get; init; } = null!;
    public string Url { get; init; } = null!;
}
