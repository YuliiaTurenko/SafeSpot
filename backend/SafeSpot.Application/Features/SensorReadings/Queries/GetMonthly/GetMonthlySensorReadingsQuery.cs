using MediatR;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.SensorReadings.Queries.GetMonthly;

public record GetMonthlySensorReadingsQuery(long SensorId) : IRequest<List<SensorReadingDto>>;