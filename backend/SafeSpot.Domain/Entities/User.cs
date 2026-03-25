using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class User : BaseEntity
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string IdentityId { get; set; } = null!;

    public virtual ICollection<UserSettings> Settings { get; set; } = new List<UserSettings>();
    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    public virtual ICollection<SavedShelter> SavedShelters { get; set; } = new List<SavedShelter>();
}
