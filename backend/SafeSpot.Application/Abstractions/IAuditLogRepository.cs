using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface IAuditLogRepository : IRepository<AuditLog>
{
}