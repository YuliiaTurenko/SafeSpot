namespace SafeSpot.Application.DTOs;

public class ShelterAnalyticsDto
{
    public double AvgTemperature { get; set; }

    public double AvgHumidity { get; set; }

    public double AvgCO2 { get; set; }

    public double AvgAirQuality { get; set; }

    public int ActiveSensors { get; set; }

    public int OfflineSensors { get; set; }
}