using MediatR;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Shelters.Commands.Delete;

public class DeleteShelterCommandHandler : IRequestHandler<DeleteShelterCommand>
{
    private readonly IShelterRepository _repo;

    public DeleteShelterCommandHandler(IShelterRepository repo)
    {
        _repo = repo;
    }

    public async Task Handle(DeleteShelterCommand request, CancellationToken ct)
    {
        //UserHasPermissionAsync
        var shelter = await _repo.GetByIdAsync(request.Id);

        _repo.Delete(shelter);
    }
}