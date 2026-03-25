using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class ShelterResourceConfiguration : IEntityTypeConfiguration<ShelterResource>
{
    public void Configure(EntityTypeBuilder<ShelterResource> builder)
    {
        builder.Property(x => x.ShelterId)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();
    }
}