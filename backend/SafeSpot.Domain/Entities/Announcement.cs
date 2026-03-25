namespace SafeSpot.Domain.Entities;

public class Announcement : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public long ShelterId { get; set; }
    public virtual Shelter Shelter { get; set; }

    public required string Title { get; set; }
    public required string Text { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
