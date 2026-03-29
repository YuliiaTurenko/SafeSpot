using Microsoft.AspNetCore.Identity;

namespace SafeSpot.Persistence.Identity;

public class ApplicationUser : IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}