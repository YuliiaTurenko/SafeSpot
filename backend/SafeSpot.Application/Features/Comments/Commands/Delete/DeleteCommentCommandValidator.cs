using FluentValidation;
using SafeSpot.Application.Features.Comments.Commands.Delete;

namespace SafeSpot.Application.Features.Comments.Commands.Delete;

public class DeleteCommentCommandValidator : AbstractValidator<DeleteCommentCommand>
{
    public DeleteCommentCommandValidator()
    {
        RuleFor(x => x.CommentId).NotEmpty();
    }
}
