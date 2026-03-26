using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class AuditLog : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public ActionType Action { get; set; }
    public EntityType EntityType { get; set; }
    public long EntityId { get; set; }
    public double OldValue { get; set; }
    public double NewValue { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
