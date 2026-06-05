using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.SavedShelters.Commands.Create;

public class CreateSavedShelterCommandHandler : IRequestHandler<CreateSavedShelterCommand, long>
{
    private readonly ISavedShelterRepository _repository;

    public CreateSavedShelterCommandHandler(ISavedShelterRepository repository)
    {
        _repository = repository;
    }

    public async Task<long> Handle(CreateSavedShelterCommand request, CancellationToken cancellationToken)
    {
        var entity = new SavedShelter
        {
            UserId = request.UserId,
            ShelterId = request.ShelterId,
            Type = SavedShelterType.Favorite
        };

        await _repository.AddAsync(entity);

        return entity.Id;
    }
}