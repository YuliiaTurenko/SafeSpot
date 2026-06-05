using MediatR;

namespace SafeSpot.Application.Features.Comments.Commands.Delete;

public record DeleteCommentCommand(
    long? UserId,
    long CommentId
) : IRequest;
