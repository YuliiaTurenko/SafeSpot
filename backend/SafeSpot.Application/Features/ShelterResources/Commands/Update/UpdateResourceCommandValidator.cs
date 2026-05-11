using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Update;

public class UpdateResourceCommandValidator : AbstractValidator<UpdateResourceCommand>
{
    private readonly IShelterResourceRepository _repo;

    public UpdateResourceCommandValidator(IShelterResourceRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.Amount).GreaterThan(-1);

        RuleFor(x => x)
            .MustAsync(ResourceExists).WithMessage("Resource not found.");
    }

    private async Task<bool> ResourceExists(UpdateResourceCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }
}
