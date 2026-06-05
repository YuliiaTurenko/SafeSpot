using MediatR;

namespace SafeSpot.Application.Features.Posts.Commands.Create;

public record CreatePostCommand(
    long UserId,
    long ShelterId,
    string Text
) : IRequest<long>;
