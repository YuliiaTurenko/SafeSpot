using FluentValidation;
using SafeSpot.Application.Features.Comments.Commands.Update;

namespace SafeSpot.Application.Features.Comments.Commands.Update;

public class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand>
{
    public UpdateCommentCommandValidator()
    {
        RuleFor(x => x.CommentId).NotEmpty();
        RuleFor(x => x.Text).NotEmpty().MaximumLength(1000);
    }
}
