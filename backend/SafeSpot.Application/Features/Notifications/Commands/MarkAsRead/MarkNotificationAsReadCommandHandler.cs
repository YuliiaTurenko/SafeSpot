using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Notifications.Commands.MarkAsRead;

public class MarkNotificationAsReadCommandHandler : IRequestHandler<MarkNotificationAsReadCommand>
{
    private readonly INotificationRepository _repo;

    public MarkNotificationAsReadCommandHandler(INotificationRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(MarkNotificationAsReadCommand request, CancellationToken ct)
    {
        var sensor = await _repo.GetByIdAsync(request.id);

        if (sensor == null)
            throw new Exception("Sensor not found");

        sensor.IsRead = true;

        _repo.Update(sensor);
    }
}