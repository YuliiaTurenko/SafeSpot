using Microsoft.AspNetCore.Identity;
using SafeSpot.Application.Abstractions;
using SafeSpot.Persistence.Identity;

namespace SafeSpot.Infrastructure.Identity;

public class UserRoleService : IUserRoleService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public UserRoleService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task AssignOperatorRoleAsync(string identityId)
    {
        var user = await _userManager.FindByIdAsync(identityId)
            ?? throw new Exception("User not found");

        if (await _userManager.IsInRoleAsync(user, "User"))
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(user, "User");
            if (!removeResult.Succeeded)
                throw new Exception("Failed to remove User role");
        }

        if (!await _userManager.IsInRoleAsync(user, "Operator"))
        {
            var addResult = await _userManager.AddToRoleAsync(user, "Operator");
            if (!addResult.Succeeded)
                throw new Exception("Failed to assign Operator role");
        }
    }

    public async Task RevokeOperatorRoleAsync(string identityId)
    {
        var user = await _userManager.FindByIdAsync(identityId)
            ?? throw new Exception("User not found");

        if (await _userManager.IsInRoleAsync(user, "Operator"))
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(user, "Operator");
            if (!removeResult.Succeeded)
                throw new Exception("Failed to remove Operator role");
        }

        if (!await _userManager.IsInRoleAsync(user, "User"))
        {
            var addResult = await _userManager.AddToRoleAsync(user, "User");
            if (!addResult.Succeeded)
                throw new Exception("Failed to assign User role");
        }
    }

    public async Task<bool> IsInRoleAsync(string identityId, string role)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        if (user == null)
            return false;

        return await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<List<string>> GetIdentityIdsByRoleAsync(string role)
    {
        var users = await _userManager.GetUsersInRoleAsync(role);
        return users.Select(x => x.Id).ToList();
    }

    public async Task<string?> GetEmailByIdentityIdAsync(string identityId)
    {
        var user = await _userManager.FindByIdAsync(identityId);
        return user?.Email;
    }
}
