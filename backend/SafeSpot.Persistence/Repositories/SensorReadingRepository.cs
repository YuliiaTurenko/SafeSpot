using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class SensorReadingRepository : Repository<SensorReading>, ISensorReadingRepository
{
    public SensorReadingRepository(ApplicationDbContext context) : base(context) { }
}