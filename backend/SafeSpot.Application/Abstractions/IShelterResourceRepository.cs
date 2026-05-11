using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

namespace SafeSpot.Application.Abstractions;

public interface IShelterResourceRepository : IRepository<ShelterResource>
{
    public Task<bool> ExistsByIdAsync(long id);
    public Task<bool> ExistsByTypeAsync(long shelterId, ResourceType type);
    public Task<List<ShelterResource>> GetAllByShelterIdAsync(long shelterId);
}