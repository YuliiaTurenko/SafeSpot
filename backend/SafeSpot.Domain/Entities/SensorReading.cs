namespace SafeSpot.Domain.Entities;

public class SensorReading : BaseEntity
{
    public long SensorId { get; set; }
    public virtual Sensor Sensor { get; set; }

    public double Value { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
