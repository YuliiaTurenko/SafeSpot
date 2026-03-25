using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class SensorConfiguration : IEntityTypeConfiguration<Sensor>
{
    public void Configure(EntityTypeBuilder<Sensor> builder)
    {
        builder.Property(x => x.ShelterId)
            .IsRequired();

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder
            .HasMany(x => x.SensorReadings)
            .WithOne(x => x.Sensor)
            .HasForeignKey(x => x.SensorId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}