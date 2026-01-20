namespace ApiTest.Application.DTOs;

public class ImageDto
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public ImageDto(string publicId, string url)
    {
        PublicId = publicId;
        Url = url;
    }
}