namespace ApiTest.Infrastructure.Providers.ExternalServices.Cloudinary;

public sealed record CloudinarySettings
{
    public const string SectionName = "Jwt";
    public string CloudName { get; init; } = null!;
    public string ApiKey { get; init; } = null!;
    public string ApiSecret { get; init; } = null!;
}
