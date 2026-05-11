using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;
using Microsoft.EntityFrameworkCore;

namespace SafeSpot.Persistence.Repositories;

public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
{
    private readonly ApplicationDbContext _db;

    public AnnouncementRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _db.Announcements.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> UserOwnsAnnouncementAsync(long userId, long announcementId)
    {
        return await _db.Announcements
            .AnyAsync(x => x.Id == announcementId && x.UserId == userId);
    }

    public async Task<List<Announcement>> GetAllByShelterIdAsync(long shelterId)
    {
        return await _db.Announcements.Where(x => x.ShelterId == shelterId).ToListAsync();
    }
}
