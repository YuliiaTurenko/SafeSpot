using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Create;

public class CreateResourceCommandValidator : AbstractValidator<CreateResourceCommand>
{
    private readonly IShelterResourceRepository _repo;

    public CreateResourceCommandValidator(IShelterResourceRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.Amount).GreaterThan(-1);

        RuleFor(x => x)
            .MustAsync(ResourceWithTypeExists).WithMessage("Resource with such type already exists for the shelter.");
    }

    private async Task<bool> ResourceWithTypeExists(CreateResourceCommand cmd, CancellationToken ct)
    {
        return !await _repo.ExistsByTypeAsync(cmd.ShelterId, cmd.Type);
    }
}
