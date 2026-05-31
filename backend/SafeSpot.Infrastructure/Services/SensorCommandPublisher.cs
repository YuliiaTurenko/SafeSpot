using SafeSpot.Application.Abstractions;
using System.Text.Json;

namespace SafeSpot.Infrastructure.Services;

public class SensorCommandPublisher : ISensorCommandPublisher
{
    private readonly IMqttCommandClient _mqttClient;

    public SensorCommandPublisher(IMqttCommandClient mqttClient)
    {
        _mqttClient = mqttClient;
    }

    public async Task PublishDisableAsync(long shelterId, long sensorId)
    {
        await PublishAsync(shelterId, sensorId, "disable");
    }

    public async Task PublishEnableAsync(long shelterId, long sensorId)
    {
        await PublishAsync(shelterId, sensorId, "enable");
    }

    private async Task PublishAsync(long shelterId, long sensorId, string action)
    {
        var topic = $"shelters/{shelterId}/sensors/{sensorId}/commands";

        //var payload = JsonSerializer.Serialize(new { action });

        //var message =
        //    new MqttApplicationMessageBuilder()
        //    .WithTopic(topic)
        //    .WithPayload(payload)
        //    .Build();

        await _mqttClient.PublishAsync(topic, JsonSerializer.Serialize(new { action }));
    }
}
