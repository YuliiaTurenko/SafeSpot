using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Shelters.Commands.Update;

public class UpdateShelterCommandValidator : AbstractValidator<UpdateShelterCommand>
{
    private readonly IShelterRepository _shelterRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;

    public UpdateShelterCommandValidator(IShelterRepository shelterRepo, ISavedShelterRepository savedShelterRepo)
    {
        _shelterRepo = shelterRepo;
        _savedShelterRepo = savedShelterRepo;

        RuleFor(x => x.Address).MaximumLength(400).NotEmpty();
        RuleFor(x => x.Latitude).NotEmpty();
        RuleFor(x => x.Longitude).NotEmpty();
        RuleFor(x => x.Capacity).GreaterThan(0);

        RuleFor(x => x).MustAsync(ShelterExists).WithMessage("Shelter not found.")
                       .MustAsync(UserHasPermission).WithMessage("You don't have permission.");
    }

    private async Task<bool> ShelterExists(UpdateShelterCommand cmd, CancellationToken ct)
    {
        return await _shelterRepo.ExistsByIdAsync(cmd.Id);
    }

    private async Task<bool> UserHasPermission(UpdateShelterCommand cmd, CancellationToken ct)
    {
        return await _savedShelterRepo.UserHasPermissionAsync(cmd.UserId, cmd.Id);
    }
}
