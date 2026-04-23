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

    public long? GetApplicationUserId()
    {
        var claim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier);
        return claim is not null && long.TryParse(claim.Value, out var id) ? id : null;
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