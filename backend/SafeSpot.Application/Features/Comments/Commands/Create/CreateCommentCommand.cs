using MediatR;

namespace SafeSpot.Application.Features.Comments.Commands.Create;

public record CreateCommentCommand(
    string? IdentityId,
    long PostId,
    string Text
) : IRequest<long>;
