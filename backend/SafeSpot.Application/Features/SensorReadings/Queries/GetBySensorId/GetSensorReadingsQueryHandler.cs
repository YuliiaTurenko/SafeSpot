using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.SensorReadings.Queries.GetBySensorId;

public class GetSensorReadingsQueryHandler : IRequestHandler<GetSensorReadingsQuery, List<SensorReadingDto>>
{
    private readonly IRepository<SensorReading> _repo;

    public GetSensorReadingsQueryHandler(IRepository<SensorReading> repo)
    {
        _repo = repo;
    }

    public async Task<List<SensorReadingDto>> Handle(GetSensorReadingsQuery request, CancellationToken ct)
    {
        var readings = await _repo.GetAllAsync();

        return readings
            .Where(x => x.SensorId == request.SensorId)
            .OrderByDescending(x => x.Timestamp)
            .Select(x =>
                new SensorReadingDto
                {
                    Id = x.Id,
                    Value = x.Value,
                    Timestamp = x.Timestamp
                })
            .ToList();
    }
}