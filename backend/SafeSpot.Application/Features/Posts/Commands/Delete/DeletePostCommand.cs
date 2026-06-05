using MediatR;

namespace SafeSpot.Application.Features.Posts.Commands.Delete;

public record DeletePostCommand(
    long UserId,
    long PostId
) : IRequest;
