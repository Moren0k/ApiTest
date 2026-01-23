using ApiTest.Application.Features.UserFeatures.DTOs;
using ApiTest.Domain.Entities.EUser;

namespace ApiTest.Application.Features.UserFeatures;

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
        
        _userRepository.Update(user);
    }

    public async Task DeleteUserAsync(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new InvalidOperationException("User not found");

        _userRepository.Remove(user);
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
        
        _userRepository.Update(user);
        
        return new UserDto(
            userId,
            user.Name,
            user.Email,
            (user.Role).ToString()
        );
    }
}