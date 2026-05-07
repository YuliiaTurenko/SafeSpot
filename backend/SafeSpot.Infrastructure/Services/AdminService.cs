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
        var userId = _userContext.GetApplicationUserId();

        var user = await _userManager.FindByIdAsync(userId.ToString());

        var body = $@"
        Admin request:
        Email: {user.Email}
        UserId: {user.Id}
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

        await _userManager.AddToRoleAsync(user, "Admin");
    }
}
