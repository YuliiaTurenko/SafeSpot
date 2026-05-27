namespace SafeSpot.Application.Abstractions;

public interface INotificationService
{
    Task CreateSensorAlertAsync(long shelterId, string title, string message);
}
