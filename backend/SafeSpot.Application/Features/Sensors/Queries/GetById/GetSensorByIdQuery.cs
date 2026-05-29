using MediatR;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Sensors.Queries.GetById;

public record GetSensorByIdQuery (long SensorId, long UserId) : IRequest<Sensor>;