using System.ComponentModel.DataAnnotations;
using ApiTest.Domain.Commons;
using ApiTest.Domain.Enums;

namespace ApiTest.Domain.Entities;

public class User : BaseEntity
{
    [StringLength(100)]public string Name { get; set; } = string.Empty;
    [EmailAddress]public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; } = UserRole.User;

    protected User() { }


    public User(string name, string email, string passwordHash, UserRole role)
    {
        Name = name;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
}