using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface IAnnouncementRepository : IRepository<Announcement>
{
    public Task<bool> ExistsByIdAsync(long id);
    public Task<bool> UserOwnsAnnouncementAsync(long userId, long announcementId);
    public Task<List<Announcement>> GetAllByShelterIdAsync(long shelterId);
}