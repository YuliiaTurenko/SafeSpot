using FluentValidation;
using SafeSpot.Application.Features.Posts.Commands.Delete;

namespace SafeSpot.Application.Features.Posts.Commands.Delete;

public class DeletePostCommandValidator : AbstractValidator<DeletePostCommand>
{
    public DeletePostCommandValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
        RuleFor(x => x.PostId).NotEmpty();
    }
}
