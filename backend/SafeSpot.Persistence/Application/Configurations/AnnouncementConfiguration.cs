using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class AnnouncementConfiguration : IEntityTypeConfiguration<Announcement>
{
    public void Configure(EntityTypeBuilder<Announcement> builder)
    {
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ShelterId)
           .IsRequired();

        builder.Property(x => x.Title)
            .HasMaxLength(100)
            .IsRequired();

         builder.Property(x => x.Text)
            .HasMaxLength(600)
            .IsRequired();

        builder.Property(x => x.ImageUrl)
           .HasMaxLength(500);

        builder
           .HasOne(x => x.User)
           .WithMany()
           .HasForeignKey(x => x.UserId)
           .OnDelete(DeleteBehavior.Restrict);

        builder
           .HasOne(x => x.Shelter)
           .WithMany()
           .HasForeignKey(x => x.ShelterId);
    }
}