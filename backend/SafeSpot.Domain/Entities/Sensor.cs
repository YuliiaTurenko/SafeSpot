using SafeSpot.Domain.Enums;

namespace SafeSpot.Domain.Entities;

public class Sensor : BaseEntity
{
    public long ShelterId { get; set; }
    public virtual Shelter Shelter { get; set; }

    public SensorType Type { get; set; }
    public SensorStatus Status { get; set; } = SensorStatus.Maintenance;
    public double MinValue { get; set; }
    public double MaxValue { get; set; }
    public DateTime InstalledAt { get; set; } = DateTime.UtcNow;

    public virtual ICollection<SensorReading> SensorReadings { get; set; } = new List<SensorReading>();
}
