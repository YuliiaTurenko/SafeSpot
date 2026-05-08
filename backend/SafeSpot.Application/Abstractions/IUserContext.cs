namespace SafeSpot.Application.Abstractions;

public interface IUserContext
{
    string? GetApplicationUserId();

    string GetLanguage();
}
