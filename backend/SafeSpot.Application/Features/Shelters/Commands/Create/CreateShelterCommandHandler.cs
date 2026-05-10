using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.Shelters.Commands.Create;

public class CreateShelterCommandHandler : IRequestHandler<CreateShelterCommand, long>
{
    private readonly IShelterRepository _repo;

    public CreateShelterCommandHandler(IShelterRepository repo)
    {
        _repo = repo;
    }

    public async Task<long> Handle(CreateShelterCommand request, CancellationToken ct)
    {
        var shelter = new Shelter
        {
            Address = request.Address,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
            Capacity = request.Capacity,
            Status = request.Status,
            Description = request.Description,
            ImageUrl = request.ImageUrl,
        };

        await _repo.AddAsync(shelter);

        return shelter.Id;
    }
}