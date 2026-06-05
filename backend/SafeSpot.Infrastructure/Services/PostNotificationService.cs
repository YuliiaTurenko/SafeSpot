using Microsoft.AspNetCore.SignalR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Infrastructure.Realtime;

namespace SafeSpot.Infrastructure.Services;

public class PostNotificationService : IPostNotificationService
{
    private readonly IHubContext<SensorHub> _hub;

    public PostNotificationService(IHubContext<SensorHub> hub)
    {
        _hub = hub;
    }

    public async Task NotifyNewPostAsync(long shelterId, object post)
    {
        await _hub.Clients.Group($"shelter-{shelterId}")
            .SendAsync("ReceivePost", post);
    }

    public async Task NotifyNewCommentAsync(long shelterId, object comment)
    {
        await _hub.Clients.Group($"shelter-{shelterId}")
            .SendAsync("ReceiveComment", comment);
    }
}
