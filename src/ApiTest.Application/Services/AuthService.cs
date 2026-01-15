using ApiTest.Application.DTOs;
using ApiTest.Application.ISecurity;
using ApiTest.Application.IServices;
using ApiTest.Domain.Entities;
using ApiTest.Domain.IRepository;

namespace ApiTest.Application.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IPasswordHasher _passwordHasher;

    public AuthService(
        IUserRepository userRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
        if (user == null)
            return null;

        var isValid = _passwordHasher.Verify(loginRequest.Password, user.PasswordHash);

        if (!isValid)
            return null;

        var userDto = new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.Role.ToString()
        );

        var token = _jwtTokenGenerator.Generate(userDto);

        return new AuthResponseDto(
            token,
            userDto
        );
    }

    public async Task<AuthResponseDto?> RegisterAsync(RegisterRequestDto registerRequest)
    {
        var exists = await _userRepository.GetByEmailAsync(registerRequest.Email);
        if (exists != null)
            return null;
        
        var passwordHash = _passwordHasher.Hash(registerRequest.Password);

        var newUser = new User(
            registerRequest.Name,
            registerRequest.Email,
            passwordHash,
            registerRequest.IsAdmin
        );
        
        await _userRepository.AddAsync(newUser);
        
        var userDto = new UserDto(
            newUser.Id,
            newUser.Name,
            newUser.Email,
            newUser.Role.ToString()
        );
        
        var token = _jwtTokenGenerator.Generate(userDto);
        
        return new AuthResponseDto(
            token,
            userDto
        );
    }
}