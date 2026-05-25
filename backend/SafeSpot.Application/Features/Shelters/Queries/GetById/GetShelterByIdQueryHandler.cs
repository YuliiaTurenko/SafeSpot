using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Shelters.Queries.GetById;

public class GetShelterByIdQueryHandler : IRequestHandler<GetShelterByIdQuery, ShelterDto>
{
    private readonly IShelterRepository _repo;

    public GetShelterByIdQueryHandler(IShelterRepository repo)
    {
        _repo = repo;
    }

    public async Task<ShelterDto> Handle(GetShelterByIdQuery request, CancellationToken ct)
    {
        var shelter = await _repo.GetByIdAsync(request.Id);

        return new ShelterDto
        {
            Id = shelter.Id,
            Address = shelter.Address,
            Latitude = shelter.Latitude,
            Longitude = shelter.Longitude,
            Capacity = shelter.Capacity,
            Status = shelter.Status,
            Description = shelter.Description,
            ImageUrl = shelter.ImageUrl,
        };
    }
}