using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetAll;

public class GetAllSheltersQueryHandler : IRequestHandler<GetAllSheltersQuery, List<ShelterPreviewDto>>
{
    private readonly IShelterRepository _repo;

    public GetAllSheltersQueryHandler(IShelterRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ShelterPreviewDto>> Handle(GetAllSheltersQuery request, CancellationToken ct)
    {
        var shelters = await _repo.GetAllAsync();

        return shelters.Select(x => new ShelterPreviewDto
        {
            Id = x.Id,
            Address = x.Address,
            Latitude = x.Latitude,
            Longitude = x.Longitude,
            Capacity = x.Capacity,
            Status = x.Status,
        }).ToList();
    }
}