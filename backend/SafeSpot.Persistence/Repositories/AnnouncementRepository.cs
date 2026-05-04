using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class AnnouncementRepository : Repository<Announcement>, IAnnouncementRepository
{
    public AnnouncementRepository(DbContext context) : base(context) { }
}
