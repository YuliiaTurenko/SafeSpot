using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace SafeSpot.Infrastructure.Realtime;

[Authorize]
public class SensorHub : Hub
{
    public async Task JoinShelter(long shelterId)
    {
        await Groups.AddToGroupAsync(
            Context.ConnectionId,
            $"shelter-{shelterId}"
        );
    }

    public async Task LeaveShelter(long shelterId)
    {
        await Groups.RemoveFromGroupAsync(
            Context.ConnectionId,
            $"shelter-{shelterId}"
        );
    }

    public async Task SendPostNotification(long shelterId, object post)
    {
        await Clients.Group($"shelter-{shelterId}").SendAsync("ReceivePost", post);
    }

    public async Task SendCommentNotification(long shelterId, object comment)
    {
        await Clients.Group($"shelter-{shelterId}").SendAsync("ReceiveComment", comment);
    }
}