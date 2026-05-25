using MediatR;

namespace SafeSpot.Application.Features.Sensors.Commands.Delete;

public record DeleteSensorCommand(
    long UserId,
    long SensorId
) : IRequest;