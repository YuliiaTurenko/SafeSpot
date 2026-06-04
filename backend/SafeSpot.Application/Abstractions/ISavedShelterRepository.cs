using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface ISavedShelterRepository : IRepository<SavedShelter>
{
    Task<bool> UserHasPermissionAsync(long userId, long shelterId);
    Task<List<long>> GetAllShelterIdsByUserIdAsync(long userId);
    Task<List<long>> GetManagementShelterIdsByUserIdAsync(long userId);
    Task<List<long>> GetManagementUserIdsAsync(long shelterId);
    Task<List<long>> GetManagementUserIdsForShelterIdsAsync(List<long> shelterIds);
    Task<bool> HasManagementLinkAsync(long userId, long shelterId);
    Task<bool> HasAnyManagementLinkAsync(long userId);
    Task DeleteManagementLinksAsync(long userId, IEnumerable<long> shelterIds);
    Task<Dictionary<long, List<long>>> GetManagementSheltersByUserIdsForShelterIdsAsync(List<long> shelterIds);
}