using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Sensors.Queries.GetByShelterId;

public class GetSensorsByShelterIdQueryHandler : IRequestHandler<GetSensorsByShelterIdQuery, List<SensorDto>>
{
    private readonly ISensorRepository _repo;

    public GetSensorsByShelterIdQueryHandler(ISensorRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<SensorDto>> Handle(GetSensorsByShelterIdQuery request, CancellationToken ct)
    {
        var sensors = await _repo
            .GetByShelterIdAsync(
                request.ShelterId);

        return sensors
            .Select(x => new SensorDto
            {
                Id = x.Id,
                Type = x.Type,
                Status = x.Status,
                MinValue = x.MinValue,
                MaxValue = x.MaxValue
            })
            .ToList();
    }
}