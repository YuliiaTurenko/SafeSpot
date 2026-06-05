using MediatR;

namespace SafeSpot.Application.Features.Posts.Commands.Update;

public record UpdatePostCommand(
    long UserId,
    long PostId,
    string Text
) : IRequest;
