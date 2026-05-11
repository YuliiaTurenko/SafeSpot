using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.ShelterResources.Commands.Delete;

public class DeleteResourceCommandValidator : AbstractValidator<DeleteResourceCommand>
{
    private readonly IShelterResourceRepository _repo;

    public DeleteResourceCommandValidator(IShelterResourceRepository repo)
    {
        _repo = repo;

        RuleFor(x => x)
            .MustAsync(ResourceExists).WithMessage("Resource not found.");
    }

    private async Task<bool> ResourceExists(DeleteResourceCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }
}
