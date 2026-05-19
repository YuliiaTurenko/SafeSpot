using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Shelters.Commands.Create;

public class CreateShelterCommandHandler : IRequestHandler<CreateShelterCommand, long>
{
    private readonly IShelterRepository _shelterRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public CreateShelterCommandHandler(IShelterRepository shelterRepo,
        ISavedShelterRepository savedShelterRepo)
    {
        _shelterRepo = shelterRepo;
        _savedShelterRepo = savedShelterRepo;
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

        await _shelterRepo.AddAsync(shelter);

        var savedShelter = new SavedShelter
        {
            UserId = request.UserId,
            ShelterId = shelter.Id,
            Type = SavedShelterType.Management,
        };

        await _savedShelterRepo.AddAsync(savedShelter);

        return shelter.Id;
    }
}