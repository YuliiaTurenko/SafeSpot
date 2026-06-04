using MediatR;

namespace SafeSpot.Application.Features.Admin.Commands.RevokeModerator;

public record RevokeModeratorCommand(
    long AdminUserId,
    long TargetUserId
) : IRequest;
