using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Sensors.Commands.Update;

public class UpdateSensorCommandHandler : IRequestHandler<UpdateSensorCommand>
{
    private readonly ISensorRepository _repo;

    public UpdateSensorCommandHandler(ISensorRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateSensorCommand request, CancellationToken ct)
    {
        var sensor = await _repo.GetByIdAsync(request.SensorId);

        if (sensor == null)
            throw new Exception("Sensor not found");

        sensor.Status = request.Status;
        sensor.MinValue = request.MinValue;
        sensor.MaxValue = request.MaxValue;

        _repo.Update(sensor);
    }
}