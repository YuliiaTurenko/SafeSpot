using MediatR;

namespace SafeSpot.Application.Features.Comments.Commands.Create;

public record CreateCommentCommand(
    long? UserId,
    long PostId,
    string Text
) : IRequest<long>;
