using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class SavedShelterConfiguration : IEntityTypeConfiguration<SavedShelter>
{
    public void Configure(EntityTypeBuilder<SavedShelter> builder)
    {
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.ShelterId)
            .IsRequired();
    }
}