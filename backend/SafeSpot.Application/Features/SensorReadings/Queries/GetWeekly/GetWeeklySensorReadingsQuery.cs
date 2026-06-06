using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.SensorReadings.Queries.GetWeekly;

public record GetWeeklySensorReadingsQuery(long SensorId) : IRequest<List<SensorReadingDto>>;