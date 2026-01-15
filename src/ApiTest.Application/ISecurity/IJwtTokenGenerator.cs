using ApiTest.Application.DTOs;

namespace ApiTest.Application.ISecurity;

public interface IJwtTokenGenerator
{
    string Generate(UserDto user);
}