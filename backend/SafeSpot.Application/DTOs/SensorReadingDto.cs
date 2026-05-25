namespace SafeSpot.Application.DTOs;

public class SensorReadingDto
{
    public long Id { get; set; }
    public double Value { get; set; }
    public DateTime Timestamp { get; set; }
}