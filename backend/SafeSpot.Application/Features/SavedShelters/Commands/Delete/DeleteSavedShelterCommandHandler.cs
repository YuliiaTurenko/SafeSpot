using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.SavedShelters.Commands.Delete;

public class DeleteSavedShelterCommandHandler : IRequestHandler<DeleteSavedShelterCommand>
{
    private readonly ISavedShelterRepository _repository;

    public DeleteSavedShelterCommandHandler(ISavedShelterRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteSavedShelterCommand request, CancellationToken cancellationToken)
    {
        var entity =
            await _repository.GetSavedShelterAsync(
                request.UserId,
                request.ShelterId,
                SavedShelterType.Favorite);

        _repository.Delete(entity);
    }
}