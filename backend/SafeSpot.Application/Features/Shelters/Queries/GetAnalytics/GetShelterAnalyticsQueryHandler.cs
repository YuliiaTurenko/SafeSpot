using MediatR;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.DTOs;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Features.Shelters.Queries.GetAnalytics;

internal class GetShelterAnalyticsQueryHandler : IRequestHandler<GetShelterAnalyticsQuery, ShelterAnalyticsDto>
{
    private readonly ISensorRepository _sensorRepo;
    private readonly ISensorReadingRepository _readingRepo;

    public GetShelterAnalyticsQueryHandler(ISensorRepository sensorRepo, ISensorReadingRepository readingRepo)
    {
        _sensorRepo = sensorRepo;
        _readingRepo = readingRepo;
    }

    public async Task<ShelterAnalyticsDto> Handle(GetShelterAnalyticsQuery request, CancellationToken ct)
    {
        var sensors = await _sensorRepo.GetByShelterIdAsync(request.ShelterId);
        var readings = await _readingRepo.GetLatestByShelterIdAsync(request.ShelterId);

        return new ShelterAnalyticsDto
        {
            AvgTemperature = readings
                .Where(x => x.Sensor.Type == SensorType.Temperature)
                .Average(x => (double?)x.Value) ?? 0,

            AvgHumidity = readings
                .Where(x => x.Sensor.Type == SensorType.Humidity)
                .Average(x => (double?)x.Value) ?? 0,

            AvgCO2 = readings
                .Where(x => x.Sensor.Type == SensorType.CO2)
                .Average(x => (double?)x.Value) ?? 0,

            AvgAirQuality = readings
                .Where(x => x.Sensor.Type == SensorType.AirQuality)
                .Average(x => (double?)x.Value) ?? 0,

            ActiveSensors = sensors
                .Count(x => x.Status == SensorStatus.Active),

            OfflineSensors = sensors
                .Count(x => x.Status == SensorStatus.Offline)
        };
    }
}