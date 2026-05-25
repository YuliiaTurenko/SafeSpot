using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Sensors.Queries.GetByShelterId;

public record GetSensorsByShelterIdQuery(long ShelterId) : IRequest<List<SensorDto>>;