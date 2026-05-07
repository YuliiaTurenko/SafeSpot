using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class SensorRepository : Repository<Sensor>, ISensorRepository
{
    public SensorRepository(ApplicationDbContext context) : base(context) { }
}
