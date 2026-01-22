using ApiTest.Domain.Entities.User;

namespace ApiTest.Application.ISecurity;

public interface IJwtTokenGenerator
{
    string Generate(User user);
}