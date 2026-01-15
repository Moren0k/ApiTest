using ApiTest.Application.IServices;
using ApiTest.Domain.Entities;
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

    public async Task<IEnumerable<User>> GetAllUsersAsync()
    {
        return await _userRepository.GetAllAsync();
    }

    public async Task<User?> GetUserByIdAsync(Guid id)
    {
        return await _userRepository.GetByIdAsync(id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _userRepository.GetByEmailAsync(email);
    }

    public async Task<User?> GetUserByNameAsync(string name)
    {
        return await _userRepository.GetByNameAsync(name);
    }
}