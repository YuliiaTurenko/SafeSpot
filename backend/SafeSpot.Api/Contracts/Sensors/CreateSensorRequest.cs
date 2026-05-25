using SafeSpot.Domain.Enums;

namespace SafeSpot.Api.Contracts.Sensors;

public class CreateSensorRequest
{
    public long ShelterId { get; set; }
    public SensorType Type { get; set; }
    public SensorStatus Status { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
}
