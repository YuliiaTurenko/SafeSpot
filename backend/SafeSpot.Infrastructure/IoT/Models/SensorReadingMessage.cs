namespace SafeSpot.Infrastructure.IoT.Models;

public class SensorReadingMessage
{
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
}