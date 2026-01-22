using ApiTest.Domain.Entities;
using ApiTest.Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTest.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");

        builder.HasKey(user => user.Id);

        builder.Property(user => user.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(user => user.Email)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasIndex(user => user.Email).IsUnique();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.Property(u => u.Role)
            .IsRequired()
            .HasConversion<int>();

        builder.HasOne(user => user.ProfileImage)
            .WithOne()
            .HasForeignKey<User>(user => user.ProfileImageId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.HasMany(user => user.Gallery)
            .WithOne()
            .HasForeignKey("UserId")
            .OnDelete(DeleteBehavior.Cascade);

        var navigation = builder.Metadata.FindNavigation(nameof(User.Gallery));
        navigation?.SetPropertyAccessMode(PropertyAccessMode.Field);
    }
}