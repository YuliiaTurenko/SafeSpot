using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.ShelterResources.Queries.GetAllByShelterId;

public class GetAllResourcesByShelterIdQueryHandler : IRequestHandler<GetAllResourcesByShelterIdQuery, List<ShelterResourceDto>>
{
    private readonly IShelterResourceRepository _repo;

    public GetAllResourcesByShelterIdQueryHandler(IShelterResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<ShelterResourceDto>> Handle(GetAllResourcesByShelterIdQuery request, CancellationToken ct)
    {
        var resources = await _repo.GetAllByShelterIdAsync(request.ShelterId);

        return resources.Select(x => new ShelterResourceDto
        {
            Id = x.Id,
            Type = x.Type,
            Status = x.Status,
            Amount = x.Amount,
        }).ToList();
    }
}