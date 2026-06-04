using MediatR;

namespace SafeSpot.Application.Features.Admin.Commands.AssignModerator;

public record AssignModeratorCommand(
    long AdminUserId,
    long TargetUserId,
    long ShelterId
) : IRequest;
