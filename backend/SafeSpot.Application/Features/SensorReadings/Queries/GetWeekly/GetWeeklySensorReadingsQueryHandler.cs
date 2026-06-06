using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;


namespace SafeSpot.Application.Features.SensorReadings.Queries.GetWeekly;

public class GetWeeklySensorReadingsQueryHandler : IRequestHandler<GetWeeklySensorReadingsQuery, List<SensorReadingDto>>
{
    private readonly ISensorReadingRepository _repo;

    public GetWeeklySensorReadingsQueryHandler(ISensorReadingRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SensorReadingDto>> Handle(GetWeeklySensorReadingsQuery request, CancellationToken ct)
    {
        var minDate = DateTime.UtcNow.AddDays(-7);
        var allReadings = await _repo.GetAllAsync();

        var filteredReadings = allReadings
            .Where(x => x.SensorId == request.SensorId && x.Timestamp >= minDate)
            .ToList();

        var weeklyData = filteredReadings
            .GroupBy(x => new
            {
                Date = x.Timestamp.Date,
                Interval = x.Timestamp.Hour / 6 
            })
            .Select(g => new SensorReadingDto
            {
                Id = 0,
                Value = Math.Round(g.Average(x => x.Value), 2),
                Timestamp = g.Key.Date.AddHours(g.Key.Interval * 6)
            })
            .OrderBy(x => x.Timestamp)
            .ToList();

        return weeklyData;
    }
}