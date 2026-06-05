using FluentValidation;
using SafeSpot.Application.Features.Posts.Commands.Create;

namespace SafeSpot.Application.Features.Posts.Commands.Create;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    public CreatePostCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.ShelterId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(2000);
    }
}
