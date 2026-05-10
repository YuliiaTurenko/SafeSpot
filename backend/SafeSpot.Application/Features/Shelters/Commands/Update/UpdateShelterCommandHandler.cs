using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Shelters.Commands.Update;

public class UpdateShelterCommandHandler : IRequestHandler<UpdateShelterCommand>
{
    private readonly IShelterRepository _repo;

    public UpdateShelterCommandHandler(IShelterRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateShelterCommand request, CancellationToken ct)
    {
        var shelter = await _repo.GetByIdAsync(request.Id);

        shelter.Address = request.Address;
        shelter.Latitude = request.Latitude;
        shelter.Longitude = request.Longitude;
        shelter.Capacity = request.Capacity;
        shelter.Status = request.Status;
        shelter.Description = request.Description;
        shelter.ImageUrl = request.ImageUrl;

        _repo.Update(shelter);
    }
}