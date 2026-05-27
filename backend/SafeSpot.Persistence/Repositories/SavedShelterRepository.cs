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
            .AnyAsync(x => x.Id == shelterId && x.UserId == userId && x.Type == SavedShelterType.Management);
    }

    public async Task<List<long>> GetAllShelterIdsByUserIdAsync(long userId)
    {
        return await _db.SavedShelters
            .Where(x => x.UserId == userId)
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
}
