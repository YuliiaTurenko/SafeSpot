using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Abstractions;

public interface ISensorRepository : IRepository<Sensor>
{
    public Task<bool> ExistsByIdAsync(long id);
    Task<bool> ExistsByTypeAsync(long shelterId, SensorType type);
    Task<List<Sensor>> GetByShelterIdAsync(long shelterId);
}