using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Exceptions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Admin.Commands.AssignModerator;

public class AssignModeratorCommandHandler : IRequestHandler<AssignModeratorCommand>
{
    private readonly IUserRepository _userRepo;
    private readonly IShelterRepository _shelterRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;
    private readonly IUserRoleService _userRoleService;

    public AssignModeratorCommandHandler(
        IUserRepository userRepo,
        IShelterRepository shelterRepo,
        ISavedShelterRepository savedShelterRepo,
        IUserRoleService userRoleService)
    {
        _userRepo = userRepo;
        _shelterRepo = shelterRepo;
        _savedShelterRepo = savedShelterRepo;
        _userRoleService = userRoleService;
    }

    public async Task Handle(AssignModeratorCommand request, CancellationToken ct)
    {
        var target = await _userRepo.GetByIdAsync(request.TargetUserId);

        if (target == null)
            throw new NotFoundException("User not found");

        if (!await _shelterRepo.ExistsByIdAsync(request.ShelterId))
            throw new NotFoundException("Shelter not found");

        if (!await _savedShelterRepo.UserHasPermissionAsync(request.AdminUserId, request.ShelterId))
            throw new ForbiddenException("You don't have permission for this shelter");

        if (await _userRoleService.IsInRoleAsync(target.IdentityId, "Admin"))
            throw new BadRequestException("Cannot assign moderator role to an admin");

        if (await _savedShelterRepo.HasManagementLinkAsync(request.TargetUserId, request.ShelterId))
            throw new BadRequestException("User is already assigned to this shelter");

        var isUser = await _userRoleService.IsInRoleAsync(target.IdentityId, "User");
        var isOperator = await _userRoleService.IsInRoleAsync(target.IdentityId, "Operator");

        if (!isUser && !isOperator)
            throw new BadRequestException("User must have User or Operator role");

        if (isUser)
            await _userRoleService.AssignOperatorRoleAsync(target.IdentityId);

        var savedShelter = new SavedShelter
        {
            UserId = request.TargetUserId,
            ShelterId = request.ShelterId,
            Type = SavedShelterType.Management,
            CreatedAt = DateTime.UtcNow
        };

        await _savedShelterRepo.AddAsync(savedShelter);
    }
}
