using ApiTest.Domain.Commons;

namespace ApiTest.Domain.Entities;

public class Image : BaseEntity
{
    public string PublicId { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public Image(string publicId, string url)
    {
        SetPublicId(publicId);
        SetUrl(url);
    }

    private void SetPublicId(string publicId)
    {
        if (string.IsNullOrWhiteSpace(publicId))
            throw new ArgumentException("PublicId cannot be empty");

        PublicId = publicId.Trim();
    }
    
    private void SetUrl(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Url cannot be empty");

        Url = url.Trim();
    }
}