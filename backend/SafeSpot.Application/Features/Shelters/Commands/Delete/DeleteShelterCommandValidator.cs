using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.Shelters.Commands.Delete;

public class DeleteShelterCommandValidator : AbstractValidator<DeleteShelterCommand>
{
    private readonly IShelterRepository _repo;

    public DeleteShelterCommandValidator(IShelterRepository repo)
    {
        _repo = repo;

        RuleFor(x => x).MustAsync(ShelterExists).WithMessage("Shelter not found.");
    }

    private async Task<bool> ShelterExists(DeleteShelterCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }
}
