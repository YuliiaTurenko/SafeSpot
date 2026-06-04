using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Admin.Queries.GetModerators;

public class GetModeratorsQueryHandler
    : IRequestHandler<GetModeratorsQuery, List<ModeratorListItemDto>>
{
    private readonly IUserRepository _userRepo;
    private readonly ISavedShelterRepository _savedShelterRepo;
    private readonly IUserRoleService _userRoleService;

    public GetModeratorsQueryHandler(
        IUserRepository userRepo,
        ISavedShelterRepository savedShelterRepo,
        IUserRoleService userRoleService)
    {
        _userRepo = userRepo;
        _savedShelterRepo = savedShelterRepo;
        _userRoleService = userRoleService;
    }

    public async Task<List<ModeratorListItemDto>> Handle(
        GetModeratorsQuery request,
        CancellationToken ct)
    {
        var adminShelterIds = await _savedShelterRepo
            .GetManagementShelterIdsByUserIdAsync(request.AdminUserId);

        if (adminShelterIds.Count == 0)
            return new List<ModeratorListItemDto>();

        var shelterAssignments = await _savedShelterRepo
            .GetManagementSheltersByUserIdsForShelterIdsAsync(adminShelterIds);

        var result = new List<ModeratorListItemDto>();

        foreach (var (userId, shelterIds) in shelterAssignments)
        {
            if (userId == request.AdminUserId)
                continue;

            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null)
                continue;

            if (!await _userRoleService.IsInRoleAsync(user.IdentityId, "Operator"))
                continue;

            var email = await _userRoleService.GetEmailByIdentityIdAsync(user.IdentityId);

            result.Add(new ModeratorListItemDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email ?? string.Empty,
                ShelterIds = shelterIds
            });
        }

        return result.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
    }
}
