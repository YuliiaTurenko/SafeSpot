using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;
using SafeSpot.Infrastructure.Realtime;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Infrastructure.Services;

public class SensorOfflineWatcherService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;

    public SensorOfflineWatcherService(
        IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider
                .GetRequiredService<ApplicationDbContext>();

            var hub = scope.ServiceProvider
                .GetRequiredService<IHubContext<SensorHub>>();

            var now = DateTime.UtcNow;

            var sensors = await db.Sensors
                .Where(x =>
                    x.LastSeenAt != null &&
                    x.Status != SensorStatus.Offline)
                .ToListAsync(stoppingToken);

            foreach (var sensor in sensors)
            {
                var diff = now - sensor.LastSeenAt.Value;

                if (diff.TotalSeconds > 30)
                {
                    sensor.Status = SensorStatus.Offline;

                    var notification = new Notification
                    {
                        UserId = 1,
                        Title = "Sensor Offline",
                        Message =
                            $"Sensor {sensor.Id} is offline",
                        Type = NotificationType.SensorAlert
                    };

                    db.Notifications.Add(notification);

                    await hub.Clients.All.SendAsync(
                        "ReceiveNotification",
                        new
                        {
                            title = notification.Title,
                            message = notification.Message
                        },
                        stoppingToken
                    );
                }
            }

            await db.SaveChangesAsync(stoppingToken);

            await Task.Delay(
                TimeSpan.FromSeconds(15),
                stoppingToken
            );
        }
    }
}