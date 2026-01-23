using ApiTest.Domain.Entities.Common;

namespace ApiTest.Domain.Entities.EUser;

public class User : BaseEntity
{
    public string Name { get; private set; } = null!;
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public UserRole Role { get; private set; }

    // Foto de Perfil (1 a 1)
    public Guid? ProfileImageId { get; private set; }
    public EImage.Image? ProfileImage { get; private set; }

    // Galería de Fotos (1 a N)
    private readonly List<EImage.Image> _gallery = [];
    public IReadOnlyCollection<EImage.Image> Gallery => _gallery.AsReadOnly();

    private User()
    {
        // Required by EF Core
    }

    public User(
        string name,
        string email,
        string passwordHash,
        UserRole role)
    {
        SetName(name);
        SetEmail(email);
        SetPasswordHash(passwordHash);
        SetRole(role);
    }

    private void SetName(string name)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name), "Name cannot be null.");
        }

        var sanitizedName = name.Trim();

        if (string.IsNullOrWhiteSpace(sanitizedName))
        {
            throw new ArgumentException("Name cannot be empty or consist only of white spaces.", nameof(name));
        }

        if (sanitizedName.Length < 3)
        {
            throw new ArgumentException("Name is too short. It must be at least 3 characters.", nameof(name));
        }

        Name = sanitizedName;
    }

    private void SetEmail(string email)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email), "Email cannot be null.");
        }

        var sanitizedEmail = email.Trim().ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(sanitizedEmail))
        {
            throw new ArgumentException("Email cannot be empty or consist only of white spaces.", nameof(email));
        }

        if (!sanitizedEmail.Contains('@') || !sanitizedEmail.Contains('.') || sanitizedEmail.Length < 5)
        {
            throw new ArgumentException($"The email format '{sanitizedEmail}' is invalid.", nameof(email));
        }

        Email = sanitizedEmail;
    }

    private void SetPasswordHash(string passwordHash)
    {
        if (passwordHash == null)
        {
            throw new ArgumentNullException(nameof(passwordHash));
        }

        var sanitizedHash = passwordHash.Trim();

        if (string.IsNullOrWhiteSpace(sanitizedHash))
        {
            throw new ArgumentException("Password hash cannot be empty.", nameof(passwordHash));
        }

        if (sanitizedHash.Length < 40)
        {
            throw new ArgumentException(
                "The provided string does not look like a valid password hash. It is too short.", nameof(passwordHash));
        }

        if (sanitizedHash.Contains(' '))
        {
            throw new ArgumentException("Password hash cannot contain internal spaces.", nameof(passwordHash));
        }

        PasswordHash = sanitizedHash;
    }

    private void SetRole(UserRole role)
    {
        if (!Enum.IsDefined(typeof(UserRole), role) || role == UserRole.None)
        {
            throw new ArgumentException("The provided role is invalid.", nameof(role));
        }

        if (Role == role)
            return;

        Role = role;
    }

    public void SetProfileImage(EImage.Image image)
    {
        ProfileImage = image ?? throw new ArgumentNullException(nameof(image));
        ProfileImageId = image.Id;
    }

    public void AddImageToGallery(EImage.Image image)
    {
        if (image == null) throw new ArgumentNullException(nameof(image));

        if (_gallery.Count >= 3)
            throw new InvalidOperationException("Gallery cannot have more than 3 images.");

        _gallery.Add(image);
    }
}