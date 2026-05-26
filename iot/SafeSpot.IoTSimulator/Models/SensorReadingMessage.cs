using System.Text.Json.Serialization;

namespace SafeSpot.IoTSimulator.Models;

public class SensorReadingMessage
{
    [JsonPropertyName("value")]
    public double Value { get; set; }

    [JsonPropertyName("timestamp")]
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
