using MediatR;

namespace SafeSpot.Application.Features.Comments.Commands.Update;

public record UpdateCommentCommand(
    long? UserId,
    long CommentId,
    string Text
) : IRequest;
