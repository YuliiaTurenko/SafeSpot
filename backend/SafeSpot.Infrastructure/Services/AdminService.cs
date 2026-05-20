using Microsoft.AspNetCore.Identity;
using SafeSpot.Application.Abstractions;
using SafeSpot.Persistence.Identity;

namespace SafeSpot.Infrastructure.Services;

public class AdminService
{
    private readonly IUserContext _userContext;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;

    public AdminService(
        IUserContext userContext,
        UserManager<ApplicationUser> userManager,
        IEmailService emailService)
    {
        _userContext = userContext;
        _userManager = userManager;
        _emailService = emailService;
    }

    public async Task SendAdminRequestAsync(string message)
    {
        var identityId = _userContext.GetApplicationUserId();

        if (string.IsNullOrEmpty(identityId))
            throw new Exception("Unauthorized");

        var identityUser = await _userManager.FindByIdAsync(identityId);

        if (identityUser == null)
            throw new Exception("User not found");

        var body = $@"
        Admin request:
        Email: {identityUser.Email}
        IdentityId: {identityUser.Id}
        Message: {message}
        ";

        await _emailService.SendAsync(
            "uliaturenko604@gmail.com",
            "Admin Role Request",
            body
        );
    }

    public async Task AssignAdminAsync(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);

        if (user == null)
            throw new Exception("User not found");

        if (await _userManager.IsInRoleAsync(user, "Admin"))
            throw new Exception("Already admin");

        if (await _userManager.IsInRoleAsync(user, "User"))
        {
            var removeResult = await _userManager.RemoveFromRoleAsync(user, "User");
            if (!removeResult.Succeeded)
            {
                var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                throw new Exception($"Failed to remove existing User role: {errors}");
            }
        }

        var addResult = await _userManager.AddToRoleAsync(user, "Admin");
        if (!addResult.Succeeded)
        {
            var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
            throw new Exception($"Failed to add Admin role: {errors}");
        }
    }
}
