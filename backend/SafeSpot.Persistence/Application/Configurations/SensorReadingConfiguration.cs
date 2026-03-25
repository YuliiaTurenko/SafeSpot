using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class SensorReadingConfiguration : IEntityTypeConfiguration<SensorReading>
{
    public void Configure(EntityTypeBuilder<SensorReading> builder)
    {
        builder.Property(x => x.SensorId)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired();
    }
}