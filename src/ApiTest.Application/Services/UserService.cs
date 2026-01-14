using ApiTest.Application.IServices;
using ApiTest.Domain.Entities;
using ApiTest.Domain.Enums;
using ApiTest.Domain.IRepository;

namespace ApiTest.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<User> RegisterAsync(string name, string email, string password)
    {
        var passwordHash = _passwordHasher.Hash(password);

        var newUser = new User(
            name: name,
            email: email,
            passwordHash: passwordHash,
            role: UserRole.User
        );

        await _userRepository.AddUserAsync(newUser);
        return newUser;
    }

    public async Task<User> LoginAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);

        var isValid = _passwordHasher.Verify(password, user.PasswordHash);
        if (!isValid)
            throw new InvalidOperationException("Invalid credentials");

        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _userRepository.GetAllUsersAsync();
    }

    public async Task<User> GetByIdAsync(Guid id)
    {
        return await _userRepository.GetUserByIdAsync(id);
    }
}