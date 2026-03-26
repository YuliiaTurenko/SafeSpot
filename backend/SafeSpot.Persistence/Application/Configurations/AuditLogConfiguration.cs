using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Application.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.EntityType)
            .IsRequired();
        
        builder.Property(x => x.EntityId)
            .IsRequired();
    }
}