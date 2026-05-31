using Microsoft.Extensions.Configuration;
using MQTTnet;
using SafeSpot.Application.Abstractions;

namespace SafeSpot.Infrastructure.Services;

public class MqttCommandClient : IMqttCommandClient
{
    private readonly IMqttClient _client;

    public MqttCommandClient(IConfiguration configuration)
    {
        var factory = new MqttClientFactory();

        _client = factory.CreateMqttClient();

        _client.ConnectAsync(
            new MqttClientOptionsBuilder()
                .WithTcpServer(
                    configuration["Mqtt:Host"],
                    int.Parse(configuration["Mqtt:Port"]))
                .Build()
        ).GetAwaiter().GetResult();
    }

    public async Task PublishAsync(
        string topic,
        string payload)
    {
        var message =
            new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(payload)
            .Build();

        await _client.PublishAsync(message);
    }
}
