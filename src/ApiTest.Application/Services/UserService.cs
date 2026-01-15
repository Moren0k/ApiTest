using ApiTest.Application.DTOs;
using ApiTest.Application.IServices;
using ApiTest.Domain.Entities;
using ApiTest.Domain.Enums;
using ApiTest.Domain.IRepository;

namespace ApiTest.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task UpdateUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        // Por ahora no hay cambios de dominio definidos
        // Ejemplo futuro:
        // user.UpdateName(...)
        // user.PromoteToAdmin()

        await _userRepository.UpdateAsync(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        await _userRepository.RemoveAsync(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();
        
        return users.Select(user => new UserDto(user.Id, user.Name, user.Email, user.Role.ToString()));
    }

    // SEARCH
    public async Task<UserDto?> GetUserByIdAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        return new UserDto(user!.Id, user.Name, user.Email, user.Role.ToString());
    }

    public async Task<UserDto?> GetUserByEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);

        return new UserDto(user!.Id, user.Name, user.Email, user.Role.ToString());
    }

    public async Task<UserDto?> GetUserByNameAsync(string name)
    {
        var user = await _userRepository.GetByNameAsync(name);
        
        return new UserDto(user!.Id, user.Name, user.Email, user.Role.ToString());
    }

    public async Task<UserDto> ChangeUserRoleAsync(Guid userId, UserRole role)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new InvalidOperationException("User not found");
        
        if (user.Role == role)
            throw new InvalidOperationException("Role already assigned");
        
        switch (role)
        {
            case UserRole.Admin:
                user.SetToAdmin();
                break;
            case UserRole.User:
                user.SetToUser();
                break;
            default:
                throw new InvalidOperationException("Role is not supported");
        }
        
        await _userRepository.UpdateAsync(user);
        
        return new UserDto(
            userId,
            user.Name,
            user.Email,
            (user.Role).ToString()
        );
    }
}