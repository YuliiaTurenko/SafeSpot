using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class SensorRepository : Repository<Sensor>, ISensorRepository
{
    private readonly ApplicationDbContext _db;

    public SensorRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _db.Sensors.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByTypeAsync(long shelterId, SensorType type)
    {
        return await _context.Sensors
            .AnyAsync(x => x.ShelterId == shelterId && x.Type == type);
    }

    public async Task<List<Sensor>> GetByShelterIdAsync(long shelterId)
    {
        return await _context.Sensors
            .Where(x => x.ShelterId == shelterId)
            .ToListAsync();
    }

}
