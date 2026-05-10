using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class ShelterConfiguration : IEntityTypeConfiguration<Shelter>
{
    public void Configure(EntityTypeBuilder<Shelter> builder)
    {
        builder.Property(x => x.Address)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Latitude)
            .IsRequired();

        builder.Property(x => x.Longitude)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(400);

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
           .HasMaxLength(500);

        builder
            .HasMany(x => x.Sensors)
            .WithOne(x => x.Shelter)
            .HasForeignKey(x => x.ShelterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Announcements)
            .WithOne(x => x.Shelter)
            .HasForeignKey(x => x.ShelterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Posts)
            .WithOne(x => x.Shelter)
            .HasForeignKey(x => x.ShelterId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasMany(x => x.Resources)
            .WithOne(x => x.Shelter)
            .HasForeignKey(x => x.ShelterId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
