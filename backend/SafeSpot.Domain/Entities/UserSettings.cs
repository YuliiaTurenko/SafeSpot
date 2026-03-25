using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class UserSettings : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public PreferredLanguage PreferredLanguage { get; set; }
    public Theme Theme { get; set; }
    public bool ReceiveNotifications { get; set; } = true;
    public bool ReceiveEmailLetters { get; set; } = true;
}
