using ApiTest.Domain.Entities.EUser;

namespace ApiTest.Application.IProviders.ISecurity;

public interface IJwtProvider
{
    string Generate(User user);
}