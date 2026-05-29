using SafeSpot.IoTSimulator.Models;
using System.Text.Json;

namespace SafeSpot.IoTSimulator.Services;

public class SensorCommandHandler
{
    private readonly List<SensorDevice> _devices;

    public SensorCommandHandler(List<SensorDevice> devices)
    {
        _devices = devices;
    }

    public Task HandleAsync(string topic, string payload)
    {
        var parts = topic.Split('/');
        long sensorId = long.Parse(parts[3]);

        var sensor = _devices.FirstOrDefault(x => x.SensorId == sensorId);
        if (sensor == null) return Task.CompletedTask;

        var command = JsonSerializer.Deserialize<SensorCommandDto>(payload);
        if (command == null) return Task.CompletedTask;

        switch (command.Action.ToLower())
        {
            case "disable":
                sensor.IsEnabled = false;
                Console.WriteLine($"Sensor {sensor.SensorId} DISABLED");
                break;
            case "enable":
                sensor.IsEnabled = true;
                Console.WriteLine($"Sensor {sensor.SensorId} ENABLED");
                break;
        }

        return Task.CompletedTask;
    }
}
