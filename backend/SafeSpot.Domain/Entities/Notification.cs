using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class Notification : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public NotificationType Type { get; set; }
    public required string Title { get; set; }
    public required string Message { get; set; }
    public bool IsRead { get; set; } = false;
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
