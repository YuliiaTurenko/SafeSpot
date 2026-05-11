using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Delete;

public class DeleteResourceCommandHandler : IRequestHandler<DeleteResourceCommand>
{
    private readonly IShelterResourceRepository _repo;

    public DeleteResourceCommandHandler(IShelterResourceRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteResourceCommand request, CancellationToken ct)
    {
        var resource = await _repo.GetByIdAsync(request.Id);

        _repo.Delete(resource);
    }
}