using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class UserSettings : BaseEntity
{
    public long UserId { get; set; }
    public virtual User User { get; set; }

    public PreferredLanguage PreferredLanguage { get; set; } = PreferredLanguage.English;
    public Theme Theme { get; set; } = Theme.Dark;
    public bool ReceiveNotifications { get; set; } = true;
    public bool ReceiveEmailLetters { get; set; } = true;
}
