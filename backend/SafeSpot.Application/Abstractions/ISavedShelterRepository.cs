using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface ISavedShelterRepository : IRepository<SavedShelter>
{
    Task<bool> UserHasPermissionAsync(long userId, long shelterId);
    Task<List<long>> GetAllShelterIdsByUserIdAsync(long userId);
    Task<List<long>> GetManagementUserIdsAsync(long shelterId);
}