using MediatR;

namespace SafeSpot.Application.Features.Posts.Commands.Create;

public record CreatePostCommand(
    string IdentityId,
    long ShelterId,
    string Text
) : IRequest<long>;
