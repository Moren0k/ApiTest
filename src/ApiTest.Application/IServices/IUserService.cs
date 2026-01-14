using ApiTest.Domain.Entities;

namespace ApiTest.Application.IServices;

public interface IUserService
{
    Task<User> RegisterAsync(string name, string email, string password);
    Task<User> LoginAsync(string email, string password);

    Task<IEnumerable<User>> GetAllAsync();
    Task<User> GetByIdAsync(Guid id); 
}