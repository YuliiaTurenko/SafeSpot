using SafeSpot.Application.Abstractions;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace SafeSpot.Infrastructure.Identity;

public class UserContext : IUserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? GetApplicationUserId()
    {
        return _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }

    public string GetLanguage()
    {
        var lang = _httpContextAccessor.HttpContext?
            .Request.Headers["Accept-Language"]
            .ToString();

        if (string.IsNullOrEmpty(lang))
            return "en";

        return lang.StartsWith("uk") ? "uk" : "en";
    }
}