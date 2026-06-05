using FluentValidation;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.SavedShelters.Commands.Create;

public class CreateSavedShelterCommandValidator : AbstractValidator<CreateSavedShelterCommand>
{
    public CreateSavedShelterCommandValidator(IShelterRepository shelterRepo, ISavedShelterRepository savedShelterRepo)
    {
        RuleFor(x => x.ShelterId)
            .MustAsync(async (id, ct) =>
                await shelterRepo
                    .ExistsByIdAsync(id))
            .WithMessage("Shelter not found");

        RuleFor(x => x)
            .MustAsync(async (command, ct) =>
                !await savedShelterRepo.IsShelterSavedAsync(
                    command.UserId,
                    command.ShelterId,
                    SavedShelterType.Favorite))
            .WithMessage("Shelter already saved");
    }
}