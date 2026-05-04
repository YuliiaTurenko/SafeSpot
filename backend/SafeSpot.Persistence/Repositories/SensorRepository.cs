using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class SensorRepository : Repository<Sensor>, ISensorRepository
{
    public SensorRepository(DbContext context) : base(context) { }
}
