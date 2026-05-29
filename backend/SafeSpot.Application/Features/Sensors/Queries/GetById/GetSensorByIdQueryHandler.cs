using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Sensors.Queries.GetById;

public class GetSensorByIdQueryHandler : IRequestHandler<GetSensorByIdQuery, Sensor>
{
    private readonly ISensorRepository _repo;

    public GetSensorByIdQueryHandler(ISensorRepository repo)
    {
        _repo = repo;
    }

    public async Task<Sensor> Handle(GetSensorByIdQuery request, CancellationToken ct)
    {
        return await _repo.GetByIdAsync(request.SensorId);
    }
}