using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Sensors.Commands.Delete;

public class DeleteSensorCommandHandler : IRequestHandler<DeleteSensorCommand>
{
    private readonly ISensorRepository _repo;

    public DeleteSensorCommandHandler(ISensorRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteSensorCommand request, CancellationToken ct)
    {
        var sensor = await _repo.GetByIdAsync( request.SensorId);

        if (sensor == null)
            throw new Exception("Sensor not found");

        _repo.Delete(sensor);
    }
}