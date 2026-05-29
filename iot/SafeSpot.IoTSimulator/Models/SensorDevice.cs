namespace SafeSpot.IoTSimulator.Models;

public class SensorDevice
{
    public int SensorId { get; set; }
    public int ShelterId { get; set; }
    public string Type { get; set; } = string.Empty;
    public double Min { get; set; }
    public double Max { get; set; }

    public double CurrentValue { get; set; } = double.NaN;
    public int Trend { get; set; } = 0;
    public bool IsOffline { get; set; } = false;
    public int OfflineCyclesLeft { get; set; } = 0;

    public bool IsEnabled { get; set; } = true;

    public string Topic => $"shelters/{ShelterId}/sensors/{SensorId}";
}
