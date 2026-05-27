using Microsoft.AspNetCore.SignalR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;
using SafeSpot.Infrastructure.Realtime;

namespace SafeSpot.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _notificationRepo;
    private readonly ISavedShelterRepository _savedRepo;
    private readonly IHubContext<SensorHub> _hub;

    public NotificationService(
        INotificationRepository notificationRepo,
        ISavedShelterRepository savedRepo,
        IHubContext<SensorHub> hub)
    {
        _notificationRepo = notificationRepo;
        _savedRepo = savedRepo;
        _hub = hub;
    }

    public async Task CreateSensorAlertAsync(long shelterId, string title, string message)
    {
        var users = await _savedRepo.GetManagementUserIdsAsync(shelterId);

        foreach (var userId in users)
        {
            var notification = new Notification
            {
                UserId = userId,
                Title = title,
                Message = message,
                Type = NotificationType.SensorAlert,
                IsRead = false
            };

            await _notificationRepo.AddAsync(notification);

            await _hub.Clients.All
                .SendAsync("ReceiveNotification", new
                {
                    notification.Title,
                    notification.Message,
                    notification.Type,
                    notification.SentAt
                });
        }
    }
}
