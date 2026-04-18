using SafeSpot.Application.Abstractions;

namespace SafeSpot.Infrastructure.Services;

public class LocalizationService : ILocalizationService
{
    private readonly Dictionary<string, Dictionary<string, string>> _resources =
        new()
        {
            ["en"] = new()
            {
                ["UserNotFound"] = "User not found",
                ["EmailNotConfirmed"] = "Email not confirmed",
                ["InvalidCredentials"] = "Invalid email or password",
                ["EmailSubject"] = "Confirm your email",
                ["EmailBody"] = "Click the link to confirm: {0}",
                ["ServerError"] = "Something went wrong",
                ["Unauthorized"] = "Unauthorized access",
                ["Forbidden"] = "Access denied. ",
                ["NotFound"] = "The requested resource was not found.",
            },
            ["uk"] = new()
            {
                ["UserNotFound"] = "Користувача не знайдено",
                ["EmailNotConfirmed"] = "Пошта не підтверджена",
                ["InvalidCredentials"] = "Невірний email або пароль",
                ["EmailSubject"] = "Підтвердження пошти",
                ["EmailBody"] = "Перейдіть за посиланням: {0}",
                ["ServerError"] = "Сталася помилка",
                ["Unauthorized"] = "Немає доступу",
                ["Forbidden"] = "Доступ заборонено.",
                ["NotFound"] = "Запитуваний ресурс не знайдено.",
            }
        };

    public string Get(string key, string lang)
    {
        if (!_resources.ContainsKey(lang))
            lang = "en";

        return _resources[lang].TryGetValue(key, out var value)
            ? value
            : key;
    }
}