using ApiTest.Application.DTOs;

namespace ApiTest.Application.SecurityJwt;

public interface IJwtTokenGenerator
{
    string Generate(UserDto user);
}