using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Sensors.Commands.Create;

public class CreateSensorCommandHandler : IRequestHandler<CreateSensorCommand, long>
{
    private readonly ISensorRepository _repo;

    public CreateSensorCommandHandler(ISensorRepository repo)
    {
        _repo = repo;
    }

    public async Task<long> Handle(CreateSensorCommand request, CancellationToken ct)
    {
        var sensor = new Sensor
        {
            ShelterId = request.ShelterId,
            Type = request.Type,
            MinValue = request.MinValue,
            MaxValue = request.MaxValue,
            Status = SensorStatus.Active
        };

        await _repo.AddAsync(sensor);

        return sensor.Id;
    }
}