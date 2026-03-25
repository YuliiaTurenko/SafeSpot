namespace SafeSpot.Domain.Entities;

public class Notification : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public required string Title { get; set; }
    public required string Message { get; set; }
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
}
