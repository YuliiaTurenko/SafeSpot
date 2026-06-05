using SafeSpot.Application.DTOs;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;

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
    Task<bool> IsShelterSavedAsync(long userId, long shelterId, SavedShelterType type);
    Task<List<SavedShelterDto>> GetFavoriteSheltersAsync(long userId);
    Task<SavedShelter?> GetSavedShelterAsync(long userId, long shelterId, SavedShelterType type);
}