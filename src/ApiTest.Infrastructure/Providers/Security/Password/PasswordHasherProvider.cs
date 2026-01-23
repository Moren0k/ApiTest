using ApiTest.Application.IProviders.ISecurity;

namespace ApiTest.Infrastructure.Providers.Security.Password;

public class PasswordHasherProvider : IPasswordHasherProvider
{
    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}