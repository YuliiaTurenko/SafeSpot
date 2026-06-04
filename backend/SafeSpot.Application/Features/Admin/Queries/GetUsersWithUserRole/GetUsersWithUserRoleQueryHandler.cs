using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;

namespace SafeSpot.Application.Features.Admin.Queries.GetUsersWithUserRole;

public class GetUsersWithUserRoleQueryHandler
    : IRequestHandler<GetUsersWithUserRoleQuery, List<AdminUserListItemDto>>
{
    private readonly IUserRepository _userRepo;
    private readonly IUserRoleService _userRoleService;

    public GetUsersWithUserRoleQueryHandler(
        IUserRepository userRepo,
        IUserRoleService userRoleService)
    {
        _userRepo = userRepo;
        _userRoleService = userRoleService;
    }

    public async Task<List<AdminUserListItemDto>> Handle(
        GetUsersWithUserRoleQuery request,
        CancellationToken ct)
    {
        var identityIds = await _userRoleService.GetIdentityIdsByRoleAsync("User");
        var users = await _userRepo.GetUsersByIdentityIdsAsync(identityIds);

        var result = new List<AdminUserListItemDto>();

        foreach (var user in users)
        {
            var email = await _userRoleService.GetEmailByIdentityIdAsync(user.IdentityId);

            result.Add(new AdminUserListItemDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = email ?? string.Empty
            });
        }

        return result.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
    }
}
