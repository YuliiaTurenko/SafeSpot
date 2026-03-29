using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class Shelter : BaseEntity
{
    public required string Location { get; set; }
    public string? Description { get; set; }
    public ShelterStatus Status { get; set; } = ShelterStatus.Available;
    public int Capacity { get; set; }
    public string? ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
    public virtual ICollection<Announcement> Announcements { get; set; } = new List<Announcement>();
    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();
    public virtual ICollection<ShelterResource> Resources { get; set; } = new List<ShelterResource>();
}
