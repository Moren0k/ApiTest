namespace ApiTest.Application.Features.ImageFeatures.DTOs;

public class ImageDto
{
    public string PublicId { get; set; }
    public string Url { get; set; }

    public ImageDto(string publicId, string url)
    {
        PublicId = publicId;
        Url = url;
    }
}