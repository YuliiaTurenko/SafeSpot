using FluentValidation;
using SafeSpot.Application.Features.Posts.Commands.Update;

namespace SafeSpot.Application.Features.Posts.Commands.Update;

public class UpdatePostCommandValidator : AbstractValidator<UpdatePostCommand>
{
    public UpdatePostCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(2000);
    }
}
