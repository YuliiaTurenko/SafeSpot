using SafeSpot.Domain.Enums;

namespace SafeSpot.Api.Contracts.Sensors;

public class UpdateSensorRequest
{
    public long SensorId { get; set; }
    public SensorStatus Status { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
}
