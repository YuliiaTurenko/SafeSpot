using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.SensorReadings.Queries.GetMonthly;

public class GetMonthlySensorReadingsQueryHandler : IRequestHandler<GetMonthlySensorReadingsQuery, List<SensorReadingDto>>
{
    private readonly ISensorReadingRepository _repo;

    public GetMonthlySensorReadingsQueryHandler(ISensorReadingRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SensorReadingDto>> Handle(GetMonthlySensorReadingsQuery request, CancellationToken ct)
    {
        var minDate = DateTime.UtcNow.AddMonths(-1);
        var allReadings = await _repo.GetAllAsync();

        var filteredReadings = allReadings
            .Where(x => x.SensorId == request.SensorId && x.Timestamp >= minDate)
            .ToList();

        var monthlyData = filteredReadings
            .GroupBy(x => x.Timestamp.Date)
            .Select(g => new SensorReadingDto
            {
                Id = 0,
                Value = Math.Round(g.Average(x => x.Value), 2),
                Timestamp = g.Key
            })
            .OrderBy(x => x.Timestamp)
            .ToList();

        return monthlyData;
    }
}