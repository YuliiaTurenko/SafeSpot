using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.SensorReadings.Queries.GetBySensorId;

public record GetSensorReadingsQuery(long SensorId) : IRequest<List<SensorReadingDto>>;