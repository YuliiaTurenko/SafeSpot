using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.SensorReadings.Commands.Create;

public class CreateSensorReadingCommandHandler : IRequestHandler<CreateSensorReadingCommand, long>
{
    private readonly ISensorReadingRepository _repo;

    public CreateSensorReadingCommandHandler(ISensorReadingRepository repo)
    {
        _repo = repo;
    }

    public async Task<long> Handle(CreateSensorReadingCommand request,CancellationToken ct)
    {
        var reading =
            new SensorReading
            {
                SensorId = request.SensorId,
                Value = request.Value
            };

        await _repo.AddAsync(reading);

        return reading.Id;
    }
}