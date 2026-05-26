using Microsoft.AspNetCore.SignalR;

namespace SafeSpot.Infrastructure.Realtime;

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
}