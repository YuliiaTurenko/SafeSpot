using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public long IdentityId { get; set; }

    public virtual ICollection<UserSettings> Settings { get; set; } = new List<UserSettings>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<SavedShelter> SavedShelters { get; set; } = new List<SavedShelter>();
}
