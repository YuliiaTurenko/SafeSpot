namespace SafeSpot.Application.Abstractions;

public interface IMqttCommandClient
{
    Task PublishAsync(string topic, string payload);
}
