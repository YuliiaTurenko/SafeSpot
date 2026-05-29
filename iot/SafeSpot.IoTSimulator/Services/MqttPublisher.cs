using System.Text;
using System.Text.Json;
using MQTTnet;
using MQTTnet.Client;
using SafeSpot.IoTSimulator.Models;

namespace SafeSpot.IoTSimulator.Services;

public class MqttPublisher : IAsyncDisposable
{
    private readonly IMqttClient _client;
    private readonly string _host;
    private readonly int _port;
    private readonly SensorCommandHandler _commandHandler;

    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        WriteIndented = false,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public bool IsConnected => _client.IsConnected;

    public MqttPublisher(string host, int port, SensorCommandHandler commandHandler)
    {
        _host = host;
        _port = port;
        _commandHandler = commandHandler;

        var factory = new MqttFactory();
        _client = factory.CreateMqttClient();

        _client.ConnectedAsync += args =>
        {
            Console.WriteLine($"MQTT connected → {_host}:{_port}");
            return Task.CompletedTask;
        };

        _client.DisconnectedAsync += async args =>
        {
            Console.WriteLine("MQTT disconnected. Reconnecting in 5s...");
            await Task.Delay(5000);
            await ConnectAsync();
        };

        _client.ApplicationMessageReceivedAsync += async e =>
        {
            var topic = e.ApplicationMessage.Topic;
            var payload = Encoding.UTF8.GetString(e.ApplicationMessage.PayloadSegment);
            Console.WriteLine($"Command received: {topic} {payload}");
            await _commandHandler.HandleAsync(topic, payload);
        };
    }

    public async Task ConnectAsync()
    {
        var options = new MqttClientOptionsBuilder()
            .WithTcpServer(_host, _port)
            .WithClientId($"iot-simulator-{Guid.NewGuid():N}")
            .WithCleanSession()
            .WithKeepAlivePeriod(TimeSpan.FromSeconds(30))
            .Build();

        try
        {
            await _client.ConnectAsync(options);
            await _client.SubscribeAsync("shelters/+/sensors/+/commands");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"! MQTT connection failed: {ex.Message}");
            Console.WriteLine("   Ensure Mosquitto is running.");
            throw;
        }
    }

    public async Task<bool> PublishAsync(SensorDevice sensor, double value)
    {
        if (!_client.IsConnected)
        {
            Console.WriteLine("! MQTT not connected, skipping publish.");
            return false;
        }

        var payload = new SensorReadingMessage
        {
            Value = value,
            Timestamp = DateTime.UtcNow
        };

        string json = JsonSerializer.Serialize(payload, JsonOptions);

        var message = new MqttApplicationMessageBuilder()
            .WithTopic(sensor.Topic)
            .WithPayload(Encoding.UTF8.GetBytes(json))
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
            .WithRetainFlag(false)
            .Build();

        await _client.PublishAsync(message);
        return true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_client.IsConnected)
            await _client.DisconnectAsync();

        _client.Dispose();
    }
}
