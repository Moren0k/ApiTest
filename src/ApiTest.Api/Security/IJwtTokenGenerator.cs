namespace ApiTest.Api.Security;

public interface IJwtTokenGenerator
{
    string Generate(Guid id);
}