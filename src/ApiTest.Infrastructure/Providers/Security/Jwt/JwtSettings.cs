namespace ApiTest.Infrastructure.Providers.Security.Jwt;

public sealed record JwtSettings
{
    public const string SectionName = "Cloudinary";
    public string Issuer { get; init; } = null!;
    public string Audience { get; init; } = null!;
    public int AccessTokenMinutes { get; init; }
}