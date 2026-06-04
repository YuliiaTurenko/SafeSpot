using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class SavedShelterRepository : Repository<SavedShelter>, ISavedShelterRepository
{
    private readonly ApplicationDbContext _db;

    public SavedShelterRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<bool> UserHasPermissionAsync(long userId, long shelterId)
    {
        return await _db.SavedShelters
            .AnyAsync(x => x.ShelterId == shelterId && x.UserId == userId && x.Type == SavedShelterType.Management);
    }

    public async Task<List<long>> GetAllShelterIdsByUserIdAsync(long userId)
    {
        return await _db.SavedShelters
            .Where(x => x.UserId == userId)
            .Select(x => x.ShelterId)
            .ToListAsync();
    }

    public async Task<List<long>> GetManagementShelterIdsByUserIdAsync(long userId)
    {
        return await _db.SavedShelters
            .Where(x => x.UserId == userId && x.Type == SavedShelterType.Management)
            .Select(x => x.ShelterId)
            .ToListAsync();
    }

    public async Task<List<long>> GetManagementUserIdsAsync(long shelterId)
    {
        return await _db.SavedShelters
            .Where(x =>
                x.ShelterId == shelterId &&
                x.Type == SavedShelterType.Management)
            .Select(x => x.UserId)
            .ToListAsync();
    }

    public async Task<List<long>> GetManagementUserIdsForShelterIdsAsync(List<long> shelterIds)
    {
        return await _db.SavedShelters
            .Where(x =>
                shelterIds.Contains(x.ShelterId) &&
                x.Type == SavedShelterType.Management)
            .Select(x => x.UserId)
            .Distinct()
            .ToListAsync();
    }

    public async Task<bool> HasManagementLinkAsync(long userId, long shelterId)
    {
        return await _db.SavedShelters
            .AnyAsync(x =>
                x.UserId == userId &&
                x.ShelterId == shelterId &&
                x.Type == SavedShelterType.Management);
    }

    public async Task<bool> HasAnyManagementLinkAsync(long userId)
    {
        return await _db.SavedShelters
            .AnyAsync(x => x.UserId == userId && x.Type == SavedShelterType.Management);
    }

    public async Task DeleteManagementLinksAsync(long userId, IEnumerable<long> shelterIds)
    {
        var links = await _db.SavedShelters
            .Where(x =>
                x.UserId == userId &&
                shelterIds.Contains(x.ShelterId) &&
                x.Type == SavedShelterType.Management)
            .ToListAsync();

        _db.SavedShelters.RemoveRange(links);
        await _db.SaveChangesAsync();
    }

    public async Task<Dictionary<long, List<long>>> GetManagementSheltersByUserIdsForShelterIdsAsync(List<long> shelterIds)
    {
        var links = await _db.SavedShelters
            .Where(x =>
                shelterIds.Contains(x.ShelterId) &&
                x.Type == SavedShelterType.Management)
            .Select(x => new { x.UserId, x.ShelterId })
            .ToListAsync();

        return links
            .GroupBy(x => x.UserId)
            .ToDictionary(g => g.Key, g => g.Select(x => x.ShelterId).Distinct().ToList());
    }
}
