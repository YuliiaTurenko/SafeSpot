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
                ["EmailAreadyConfirmed"] = "Email already confirmed",
                ["InvalidPassword"] = "Invalid password",
                ["BadRequest"] = "Invalid request. The server cannot process this command.",
                ["ServerError"] = "Something went wrong.",
                ["Unauthorized"] = "Unauthorized access.",
                ["Forbidden"] = "Access denied.",
                ["NotFound"] = "The requested resource was not found.",
                ["RegistrationFailed"] = "Registration failed",
            },
            ["uk"] = new()
            {
                ["UserNotFound"] = "Користувача не знайдено",
                ["EmailNotConfirmed"] = "Пошта не підтверджена",
                ["EmailAreadyConfirmed"] = "Електронна адреса вже підтверджена",
                ["InvalidPassword"] = "Невірний пароль",
                ["ServerError"] = "Сталася помилка.",
                ["BadRequest"] = "Неправильний запит. Сервер не може опрацювати цю команду.",
                ["Unauthorized"] = "Немає доступу.",
                ["Forbidden"] = "Доступ заборонено.",
                ["NotFound"] = "Запитуваний ресурс не знайдено.",
                ["RegistrationFailed"] = "Реєстрація не вдалася",
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