using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetByUserId;

public class GetSheltersByUserIdQueryHandler : IRequestHandler<GetSheltersByUserIdQuery, List<ShelterDto>>
{
    private readonly IShelterRepository _shelterRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public GetSheltersByUserIdQueryHandler(IShelterRepository shelterRepo,
        ISavedShelterRepository savedShelterRepo)
    {
        _shelterRepo = shelterRepo;
        _savedShelterRepo = savedShelterRepo;
    }

    public async Task<List<ShelterDto>> Handle(GetSheltersByUserIdQuery request, CancellationToken ct)
    {
        List<long> shelterIds = await _savedShelterRepo.GetAllShelterIdsByUserIdAsync(request.userId);

        var shelters = await _shelterRepo.GetAllSheltersByIdsAsync(shelterIds);

        return shelters.Select(x => new ShelterDto
        {
            Id = x.Id,
            Address = x.Address,
            Latitude = x.Latitude,
            Longitude = x.Longitude,
            Capacity = x.Capacity,
            Status = x.Status,
            Description = x.Description,
            ImageUrl = x.ImageUrl,
        }).ToList();
    }
}