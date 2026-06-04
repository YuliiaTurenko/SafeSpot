using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;

namespace SafeSpot.Application.Features.Admin.Commands.RevokeModerator;

public class RevokeModeratorCommandHandler : IRequestHandler<RevokeModeratorCommand>
{
    private readonly IUserRepository _userRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;
    private readonly IUserRoleService _userRoleService;

    public RevokeModeratorCommandHandler(
        IUserRepository userRepo,
        ISavedShelterRepository savedShelterRepo,
        IUserRoleService userRoleService)
    {
        _userRepo = userRepo;
        _savedShelterRepo = savedShelterRepo;
        _userRoleService = userRoleService;
    }

    public async Task Handle(RevokeModeratorCommand request, CancellationToken ct)
    {
        var target = await _userRepo.GetByIdAsync(request.TargetUserId);

        if (target == null)
            throw new NotFoundException("User not found");

        if (!await _userRoleService.IsInRoleAsync(target.IdentityId, "Operator"))
            throw new BadRequestException("User is not a moderator");

        var adminShelterIds = await _savedShelterRepo.GetManagementShelterIdsByUserIdAsync(request.AdminUserId);
        var targetShelterIds = await _savedShelterRepo.GetManagementShelterIdsByUserIdAsync(request.TargetUserId);

        var sharedShelterIds = adminShelterIds.Intersect(targetShelterIds).ToList();

        if (sharedShelterIds.Count == 0)
            throw new ForbiddenException("Moderator is not assigned to your shelters");

        await _savedShelterRepo.DeleteManagementLinksAsync(request.TargetUserId, sharedShelterIds);

        if (!await _savedShelterRepo.HasAnyManagementLinkAsync(request.TargetUserId))
            await _userRoleService.RevokeOperatorRoleAsync(target.IdentityId);
    }
}
