using MediatR;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Sensors.Commands.Update;

public record UpdateSensorCommand(
    long UserId,
    long SensorId,
    SensorStatus Status,
    double MinValue,
    double MaxValue
) : IRequest;