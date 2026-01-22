using ApiTest.Domain.Entities;
using ApiTest.Domain.Entities.Image;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiTest.Infrastructure.Persistence.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.ToTable("Images");

        builder.HasKey(i => i.Id);

        builder.Property(i => i.PublicId)
            .IsRequired()
            .HasMaxLength(200); 

        builder.Property(i => i.Url)
            .IsRequired()
            .HasMaxLength(500);
            
        builder.Property(i => i.CreatedAt)
            .IsRequired();
    }
}