using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class SensorReadingRepository : Repository<SensorReading>, ISensorReadingRepository
{
    private readonly ApplicationDbContext _db;

    public SensorReadingRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<List<SensorReading>> GetLatestByShelterIdAsync(long shelterId)
    {
        return await _db.SensorReadings
            .Include(x => x.Sensor)
            .Where(x => x.Sensor.ShelterId == shelterId)
            .GroupBy(x => x.SensorId)
            .Select(g => g.OrderByDescending(x => x.Timestamp)
            .First()).ToListAsync();
    }
}