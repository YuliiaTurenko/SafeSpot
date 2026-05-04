using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class SensorReadingRepository : Repository<SensorReading>, ISensorReadingRepository
{
    public SensorReadingRepository(DbContext context) : base(context) { }
}