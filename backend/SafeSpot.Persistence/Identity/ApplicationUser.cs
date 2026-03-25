using Microsoft.AspNetCore.Identity;

namespace SafeSpot.Persistence.Identity;

public class ApplicationUser : IdentityUser<long>
{
    public DateTime CreatedAt { get; set; }
}