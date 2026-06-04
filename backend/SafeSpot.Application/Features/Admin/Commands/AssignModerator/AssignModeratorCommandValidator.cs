using FluentValidation;

namespace SafeSpot.Application.Features.Admin.Commands.AssignModerator;

public class AssignModeratorCommandValidator : AbstractValidator<AssignModeratorCommand>
{
    public AssignModeratorCommandValidator()
    {
        RuleFor(x => x.AdminUserId).GreaterThan(0);
        RuleFor(x => x.TargetUserId).GreaterThan(0);
        RuleFor(x => x.ShelterId).GreaterThan(0);
        RuleFor(x => x).Must(x => x.AdminUserId != x.TargetUserId)
            .WithMessage("Cannot assign yourself as moderator");
    }
}
