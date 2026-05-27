using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.DTOs;

public class SensorDto
{
    public long Id { get; set; }
    public SensorType Type { get; set; }
    public SensorStatus Status { get; set; }
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
    public double? CurrentValue { get; set; }
}