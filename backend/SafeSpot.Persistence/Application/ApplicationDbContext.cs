using SafeSpot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace SafeSpot.Persistence.Application;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Announcement> Announcements { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Notification> Notifications { get; set; }
    public DbSet<Post> Posts { get; set; }
    public DbSet<SavedShelter> SavedShelters { get; set; }
    public DbSet<Sensor> Sensors { get; set; }
    public DbSet<SensorReading> SensorReadings { get; set; }
    public DbSet<Shelter> Shelters { get; set; }
    public DbSet<ShelterResource> ShelterResources { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserSettings> Settings { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema("SafeSpot");
        builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly,
            t => t.Namespace == "Persistence.Application.Configurations");
    }
}
