using FluentValidation;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Application.Features.User.Commands.Update;

public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    private readonly IUserRepository _repo;

    public UpdateUserCommandValidator(IUserRepository repo)
    {
        _repo = repo;

        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(100);

        RuleFor(x => x.LastName).NotEmpty().MaximumLength(100);

        RuleFor(x => x)
            .MustAsync(UserExists).WithMessage("User not found.");
    }

    private async Task<bool> UserExists(UpdateUserCommand cmd, CancellationToken ct)
    {
        return await _repo.ExistsByIdAsync(cmd.Id);
    }
}