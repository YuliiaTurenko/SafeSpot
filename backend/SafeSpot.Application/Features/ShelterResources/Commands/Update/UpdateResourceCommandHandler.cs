using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Update;

public class UpdateResourceCommandHandler : IRequestHandler<UpdateResourceCommand>
{
    private readonly IShelterResourceRepository _repo;

    public UpdateResourceCommandHandler(IShelterResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(UpdateResourceCommand request, CancellationToken ct)
    {
        var resource = await _repo.GetByIdAsync(request.Id);

        resource.Status = request.Status;
        resource.Amount = request.Amount;

        _repo.Update(resource);
    }
}