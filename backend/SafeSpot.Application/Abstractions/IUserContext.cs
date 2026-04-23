namespace SafeSpot.Application.Abstractions;

public interface IUserContext
{
    long? GetApplicationUserId();

    string GetLanguage();
}
