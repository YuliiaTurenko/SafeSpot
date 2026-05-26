using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MQTTnet;
using SafeSpot.Infrastructure.Realtime;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;
using SafeSpot.Infrastructure.IoT.Models;
using SafeSpot.Persistence.Application;
using System.Text;
using System.Text.Json;

namespace SafeSpot.Infrastructure.IoT;

public class MqttHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private IMqttClient _client;
    private readonly IHubContext<SensorHub> _hub;

    public MqttHostedService(IServiceScopeFactory scopeFactory, IHubContext<SensorHub> hub)
    {
        _scopeFactory = scopeFactory;
        _hub = hub;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new MqttClientFactory();

        _client = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithTcpServer("localhost", 1883)
            .Build();

        _client.ApplicationMessageReceivedAsync += HandleMessage;

        await _client.ConnectAsync(options, stoppingToken);

        await _client.SubscribeAsync("shelters/+/sensors/+");

        Console.WriteLine("MQTT connected");
    }

    private async Task HandleMessage(MqttApplicationMessageReceivedEventArgs args)
    {
        try
        {
            var topic = args.ApplicationMessage.Topic;
            var parts = topic.Split('/');

            long shelterId = long.Parse(parts[1]);
            long sensorId = long.Parse(parts[3]);

            var payload = Encoding.UTF8.GetString(args.ApplicationMessage.Payload);

            var message = JsonSerializer.Deserialize<SensorReadingMessage>(payload);

            if (message == null)
                return;

            using var scope = _scopeFactory.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var sensor = await db.Sensors.FindAsync(sensorId);

            if (sensor == null)
                return;

            var reading = new SensorReading
            {
                SensorId = sensorId,
                Value = message.Value,
                Timestamp = message.Timestamp
            };

            db.SensorReadings.Add(reading);

            sensor.Status = SensorStatus.Active;

            await _hub.Clients.Group($"shelter-{sensor.ShelterId}")
                .SendAsync("ReceiveSensorReading", new
                {
                    sensorId = sensor.Id,
                    shelterId = sensor.ShelterId,
                    type = sensor.Type,
                    value = reading.Value,
                    timestamp = reading.Timestamp,
                    status = sensor.Status
                });

            await db.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}