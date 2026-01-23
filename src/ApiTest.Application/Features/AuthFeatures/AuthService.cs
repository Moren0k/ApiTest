using ApiTest.Application.Features.AuthFeatures.DTOs;
using ApiTest.Application.Features.UserFeatures.DTOs;
using ApiTest.Application.IProviders.ISecurity;
using ApiTest.Domain.Entities.Common;
using ApiTest.Domain.Entities.EUser;

namespace ApiTest.Application.Features.AuthFeatures;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasherProvider _passwordHasherProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AuthService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider,
        IPasswordHasherProvider passwordHasherProvider,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _jwtProvider = jwtProvider;
        _passwordHasherProvider = passwordHasherProvider;
        _unitOfWork = unitOfWork;
    }

    public async Task<AuthResponseDto?> LoginAsync(LoginRequestDto loginRequest)
    {
        var user = await _userRepository.GetByEmailAsync(loginRequest.Email);
        if (user == null)
            return null;

        var isValid = _passwordHasherProvider.Verify(loginRequest.Password, user.PasswordHash);

        if (!isValid)
            return null;

        var userDto = new UserDto(
            user.Id,
            user.Name,
            user.Email,
            user.Role.ToString()
        );

        var token = _jwtProvider.Generate(user);

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
        
        var passwordHash = _passwordHasherProvider.Hash(registerRequest.Password);

        var newUser = new User(
            registerRequest.Name,
            registerRequest.Email,
            passwordHash,
            registerRequest.Role
        );
        
        _userRepository.Add(newUser);
        await _unitOfWork.SaveChangesAsync();
        
        var userDto = new UserDto(
            newUser.Id,
            newUser.Name,
            newUser.Email,
            newUser.Role.ToString()
        );
        
        var token = _jwtProvider.Generate(newUser);
        
        return new AuthResponseDto(
            token,
            userDto
        );
    }
}