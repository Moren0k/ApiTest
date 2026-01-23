namespace ApiTest.Application.IProviders.ISecurity;

public interface IPasswordHasherProvider
{
    string Hash(string password);
    bool Verify(string password, string passwordHash);
}