using FluentValidation;

namespace SafeSpot.Application.Features.Admin.Commands.RevokeModerator;

public class RevokeModeratorCommandValidator : AbstractValidator<RevokeModeratorCommand>
{
    public RevokeModeratorCommandValidator()
    {
        RuleFor(x => x.AdminUserId).GreaterThan(0);
        RuleFor(x => x.TargetUserId).GreaterThan(0);
    }
}
