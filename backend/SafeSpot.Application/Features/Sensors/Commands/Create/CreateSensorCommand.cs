using MediatR;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Sensors.Commands.Create;

public record CreateSensorCommand(
    long UserId,
    long ShelterId,
    SensorType Type,
    double MinValue,
    double MaxValue
) : IRequest<long>;