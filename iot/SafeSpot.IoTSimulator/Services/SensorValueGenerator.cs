using SafeSpot.IoTSimulator.Models;

namespace SafeSpot.IoTSimulator.Services;

public class SensorValueGenerator
{
    private readonly Random _rng = new();

    private const double OfflineProbability = 0.02;
    private const double InvalidReadingProb = 0.03;
    private const double CriticalReadingProb = 0.05;
    private const double StepFraction = 0.08;
    private readonly Dictionary<int, double> _shelterOccupancy = new();

    public double? Generate(SensorDevice sensor)
    {
        if (sensor.IsOffline)
        {
            sensor.OfflineCyclesLeft--;

            if (sensor.OfflineCyclesLeft <= 0)
            {
                sensor.IsOffline = false;
                LogEvent(sensor, "Back ONLINE");
            }
            else
            {
                LogEvent(sensor, $"OFFLINE ({sensor.OfflineCyclesLeft} cycles left)");
            }
            return null; 
        }

        if (_rng.NextDouble() < OfflineProbability)
        {
            sensor.IsOffline = true;
            sensor.OfflineCyclesLeft = _rng.Next(2, 6);
            LogEvent(sensor, $"Going OFFLINE for {sensor.OfflineCyclesLeft} cycles");
            return null;
        }

        if (_rng.NextDouble() < InvalidReadingProb)
        {
            double invalidValue = _rng.Next(0, 2) == 0 ? -100 : 99999;
            LogEvent(sensor, $"INVALID reading: {invalidValue}");
            return invalidValue;
        }

        if (double.IsNaN(sensor.CurrentValue))
        {
            sensor.CurrentValue = Lerp(sensor.Min, sensor.Max, _rng.NextDouble());
        }

        if (_rng.NextDouble() < CriticalReadingProb && sensor.Type == "CO2")
        {
            double spike = sensor.Max * 1.1;
            LogEvent(sensor, $"CRITICAL CO₂ spike: {spike:F1}");
            sensor.CurrentValue = spike;
            return Math.Round(spike, 2);
        }

        double newValue = ComputeNextValue(sensor);
        sensor.CurrentValue = newValue;
        return Math.Round(newValue, 2);
    }

    private double ComputeNextValue(SensorDevice sensor)
    {
        double range = sensor.Max - sensor.Min;
        double maxStep = range * StepFraction;

        return sensor.Type switch
        {
            "Occupancy" => NextOccupancy(sensor, maxStep),
            "Temperature" => NextSmooth(sensor, maxStep),
            "Humidity" => NextSmooth(sensor, maxStep),
            "CO2" => NextCO2(sensor, maxStep),
            "AirQuality" => NextAirQuality(sensor, maxStep),
            _ => NextSmooth(sensor, maxStep)
        };
    }

    private double NextSmooth(SensorDevice sensor, double maxStep)
    {
        if (_rng.NextDouble() < 0.20)
            sensor.Trend = _rng.Next(-1, 2);

        double delta = sensor.Trend * maxStep * _rng.NextDouble()
                       + ((_rng.NextDouble() - 0.5) * maxStep * 0.3);

        return Clamp(sensor.CurrentValue + delta, sensor.Min, sensor.Max);
    }

    private double NextOccupancy(SensorDevice sensor, double maxStep)
    {
        double bigStep = maxStep * 1.5;

        if (_rng.NextDouble() < 0.15)
            sensor.Trend = _rng.Next(-1, 2);

        double delta = sensor.Trend * bigStep * _rng.NextDouble()
                       + ((_rng.NextDouble() - 0.5) * bigStep * 0.2);

        double random = Clamp(sensor.CurrentValue + delta, sensor.Min, sensor.Max);
        double next = Math.Round(random);

        _shelterOccupancy[sensor.ShelterId] = next / sensor.Max;

        return next;
    }

    private double NextCO2(SensorDevice sensor, double maxStep)
    {
        double occupancyRatio = _shelterOccupancy.GetValueOrDefault(sensor.ShelterId, 0.3);
        double targetNorm = 0.2 + occupancyRatio * 0.7;
        double target = sensor.Min + targetNorm * (sensor.Max - sensor.Min);
        double pull = (target - sensor.CurrentValue) * 0.15;
        double noise = (_rng.NextDouble() - 0.5) * maxStep * 0.4;

        return Clamp(sensor.CurrentValue + pull + noise, sensor.Min, sensor.Max);
    }

    private double NextAirQuality(SensorDevice sensor, double maxStep)
    {
        double occupancyRatio = _shelterOccupancy.GetValueOrDefault(sensor.ShelterId, 0.3);
        double targetNorm = 0.1 + occupancyRatio * 0.6;
        double target = sensor.Min + targetNorm * (sensor.Max - sensor.Min);
        double pull = (target - sensor.CurrentValue) * 0.12;
        double noise = (_rng.NextDouble() - 0.5) * maxStep * 0.3;

        return Clamp(sensor.CurrentValue + pull + noise, sensor.Min, sensor.Max);
    }

    private static double Lerp(double a, double b, double t) => a + (b - a) * t;
    
    private static double Clamp(double v, double min, double max)
        => v < min ? min : v > max ? max : v;

    private static void LogEvent(SensorDevice sensor, string msg)
        => Console.WriteLine($"  [{sensor.Type,-12}] Shelter {sensor.ShelterId} / Sensor {sensor.SensorId:D3} -> {msg}");
}
