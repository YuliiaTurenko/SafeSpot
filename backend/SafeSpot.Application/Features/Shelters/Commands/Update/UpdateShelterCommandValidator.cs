using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Shelters.Commands.Update;

public class UpdateShelterCommandValidator : AbstractValidator<UpdateShelterCommand>
{
    private readonly IShelterRepository _repo;

    public UpdateShelterCommandValidator(IShelterRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.Address).MaximumLength(400).NotEmpty();
        RuleFor(x => x.Latitude).NotEmpty();
        RuleFor(x => x.Longitude).NotEmpty();
        RuleFor(x => x.Capacity).GreaterThan(0);

        RuleFor(x => x).MustAsync(ShelterExists).WithMessage("Shelter not found.");
    }

    private async Task<bool> ShelterExists(UpdateShelterCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }
}
