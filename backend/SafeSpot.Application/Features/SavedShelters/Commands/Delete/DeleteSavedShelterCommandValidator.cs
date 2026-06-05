using FluentValidation;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.SavedShelters.Commands.Delete;

public class DeleteSavedShelterCommandValidator : AbstractValidator<DeleteSavedShelterCommand>
{
    public DeleteSavedShelterCommandValidator(ISavedShelterRepository savedShelterRepo)
    {
        RuleFor(x => x)
            .MustAsync(async (command, ct) =>
                await savedShelterRepo.IsShelterSavedAsync(
                    command.UserId,
                    command.ShelterId,
                    SavedShelterType.Favorite))
            .WithMessage("Saved shelter not found");
    }
}