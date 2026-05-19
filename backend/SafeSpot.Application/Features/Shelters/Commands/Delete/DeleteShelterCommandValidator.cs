using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Shelters.Commands.Delete;

public class DeleteShelterCommandValidator : AbstractValidator<DeleteShelterCommand>
{
    private readonly IShelterRepository _shelterRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public DeleteShelterCommandValidator(IShelterRepository shelterRepo, ISavedShelterRepository savedShelterRepo)
    {
        _shelterRepo = shelterRepo;
        _savedShelterRepo = savedShelterRepo;

        RuleFor(x => x).MustAsync(ShelterExists).WithMessage("Shelter not found.")
                       .MustAsync(UserHasPermission).WithMessage("You don't have permission.");
    }

    private async Task<bool> ShelterExists(DeleteShelterCommand cmd, CancellationToken ct)
    {
        return await _shelterRepo.ExistsByIdAsync(cmd.Id);
    }

    private async Task<bool> UserHasPermission(DeleteShelterCommand cmd, CancellationToken ct)
    {
        return await _savedShelterRepo.UserHasPermissionAsync(cmd.UserId, cmd.Id);
    }
}
