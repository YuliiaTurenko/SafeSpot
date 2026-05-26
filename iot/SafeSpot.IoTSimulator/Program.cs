using System.Text.Json;
using Microsoft.Extensions.Configuration;
using SafeSpot.IoTSimulator.Models;
using SafeSpot.IoTSimulator.Services;

Console.OutputEncoding = System.Text.Encoding.UTF8;

var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

string mqttHost = config["Mqtt:Host"] ?? "localhost";
int mqttPort = int.Parse(config["Mqtt:Port"] ?? "1883");
int intervalMin = int.Parse(config["Simulation:IntervalMinSeconds"] ?? "2");
int intervalMax = int.Parse(config["Simulation:IntervalMaxSeconds"] ?? "10");

string sensorsPath = Path.Combine(AppContext.BaseDirectory, "Config", "sensors.json");

if (!File.Exists(sensorsPath))
{
    Console.WriteLine($"ERROR: sensors.json not found at: {sensorsPath}");
    return;
}

var sensorOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
var dataConfig = JsonSerializer.Deserialize<SensorsConfig>(await File.ReadAllTextAsync(sensorsPath), sensorOptions)!;
var sensors = dataConfig.Devices;

PrintBanner(sensors, mqttHost, mqttPort);

var generator = new SensorValueGenerator();
var rng = new Random();

await using var publisher = new MqttPublisher(mqttHost, mqttPort);
using var cts = new CancellationTokenSource();
Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    Console.WriteLine("\nShutting down simulator...");
    cts.Cancel();
};

try
{
    await publisher.ConnectAsync();
}
catch
{
    Console.WriteLine("Tip: start Mosquitto with");
    Environment.Exit(1);
}

int cycle = 0;
while (!cts.Token.IsCancellationRequested)
{
    cycle++;
    Console.WriteLine();
    Console.WriteLine($"--- Cycle #{cycle:D4}  {DateTime.UtcNow:HH:mm:ss} UTC ---");

    int published = 0;
    int skipped = 0;

    foreach (var sensor in sensors)
    {
        double? value = generator.Generate(sensor);

        if (value is null)
        {
            skipped++;
            continue;
        }

        bool ok = await publisher.PublishAsync(sensor, value.Value);

        if (ok)
        {
            published++;
            PrintPublished(sensor, value.Value);
        }
    }

    Console.WriteLine($"  Published: {published}  |  Skipped (offline): {skipped}");

    int delay = rng.Next(intervalMin, intervalMax + 1);
    Console.WriteLine($"  Next cycle in {delay}s...");

    try
    {
        await Task.Delay(TimeSpan.FromSeconds(delay), cts.Token);
    }
    catch (OperationCanceledException)
    {
        break;
    }
}

Console.WriteLine("Simulator stopped.");

static void PrintBanner(List<SensorDevice> sensors, string host, int port)
{
    Console.WriteLine($"  MQTT Broker : {host}:{port}");
    Console.WriteLine($"  Sensors     : {sensors.Count} devices");
    Console.WriteLine();
    Console.WriteLine("  Loaded sensors:");
    foreach (var s in sensors)
        Console.WriteLine($"    [{s.SensorId:D3}] Shelter {s.ShelterId} -> {s.Type,-12} ({s.Min}-{s.Max})  topic: {s.Topic}");
    Console.WriteLine();
}

static void PrintPublished(SensorDevice sensor, double value)
{
    string unit = sensor.Type switch
    {
        "Temperature" => "C",
        "Humidity" => "%",
        "CO2" => "ppm",
        "AirQuality" => "AQI",
        "Occupancy" => "persons",
        _ => ""
    };

    bool isCritical = (sensor.Type == "CO2" && value > 1200) || value is -100 or 99999;
    string warn = isCritical ? " [CRITICAL]" : "";

    Console.WriteLine($"  [{sensor.Type,-12}] " +
                      $"Shelter {sensor.ShelterId} / Sensor {sensor.SensorId:D3} -> " +
                      $"{value,8:F2} {unit,-8}  topic: {sensor.Topic}{warn}");
}
