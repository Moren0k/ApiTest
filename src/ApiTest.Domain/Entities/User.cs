using ApiTest.Domain.Commons;
using ApiTest.Domain.Enums;

namespace ApiTest.Domain.Entities;

public class User : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; } = UserRole.User;

    protected User() { } // EF

    public User(
        string name,
        string email,
        string passwordHash,
        bool isAdmin)
    {
        SetName(name);
        SetEmail(email);
        SetPasswordHash(passwordHash);
        Role = isAdmin ? UserRole.Admin : UserRole.User;
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Name cannot be empty");

        Name = name.Trim();
    }

    private void SetEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty");

        if (!email.Contains("@"))
            throw new ArgumentException("Email is invalid");

        Email = email.Trim().ToLowerInvariant();
    }

    private void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("PasswordHash cannot be empty");

        PasswordHash = passwordHash;
    }
    
    public void PromoteToAdmin()
    {
        if (Role == UserRole.Admin)
            return;

        Role = UserRole.Admin;
    }
    
    public void DemoteToUser()
    {
        if (Role == UserRole.User)
            return;
        
        Role = UserRole.User;
    }
}