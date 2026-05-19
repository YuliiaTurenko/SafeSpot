using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface ISavedShelterRepository : IRepository<SavedShelter>
{
    public Task<bool> UserHasPermissionAsync(long userId, long shelterId);
    public Task<List<long>> GetAllShelterIdsByUserIdAsync(long userId);
}