namespace SafeSpot.Api.Contracts.SensorReadings;

public class CreateSensorReadingRequest
{
    public long SensorId { get; set; }
    public double Value { get; set; }
}
