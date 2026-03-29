namespace SafeSpot.Domain.Entities;

public class Comment : BaseEntity
{
    public long? UserId { get; set; }
    public virtual User? User { get; set; }

    public long PostId { get; set; }
    public virtual Post Post { get; set; }

    public required string Text { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
