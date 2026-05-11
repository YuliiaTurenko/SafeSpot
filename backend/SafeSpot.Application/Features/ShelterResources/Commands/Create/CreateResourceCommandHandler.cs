using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Create;

public class CreateResourceCommandHandler : IRequestHandler<CreateResourceCommand, long>
{
    private readonly IShelterResourceRepository _repo;

    public CreateResourceCommandHandler(IShelterResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task<long> Handle(CreateResourceCommand request, CancellationToken ct)
    {
        var resource = new ShelterResource
        {
            ShelterId = request.ShelterId,
            Type = request.Type,
            Status = request.Status,
            Amount = request.Amount,
        };

        await _repo.AddAsync(resource);

        return resource.Id;
    }
}