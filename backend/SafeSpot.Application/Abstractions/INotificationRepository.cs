using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface INotificationRepository : IRepository<Notification>
{
    Task<List<Notification>> GetByUserIdAsync(long userId);
    Task<int> GetUnreadCountAsync(long userId);
}