namespace SafeSpot.Application.Abstractions;

public interface ILocalizationService
{
    string Get(string key, string lang);
}