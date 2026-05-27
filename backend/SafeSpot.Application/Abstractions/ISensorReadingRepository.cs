using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface ISensorReadingRepository : IRepository<SensorReading>
{
    Task<List<SensorReading>> GetLatestByShelterIdAsync(long shelterId);
}