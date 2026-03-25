namespace SafeSpot.Domain.Entities;

public class SavedShelter : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public long GroupId { get; set; }
    public virtual Shelter Shelter { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
