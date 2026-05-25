using MediatR;

namespace SafeSpot.Application.Features.SensorReadings.Commands.Create;

public record CreateSensorReadingCommand(
    long SensorId,
    double Value
) : IRequest<long>;
