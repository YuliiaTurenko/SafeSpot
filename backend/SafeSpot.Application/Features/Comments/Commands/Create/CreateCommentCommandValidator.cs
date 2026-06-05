using FluentValidation;
using SafeSpot.Application.Features.Comments.Commands.Create;

namespace SafeSpot.Application.Features.Comments.Commands.Create;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(x => x.PostId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
    }
}
