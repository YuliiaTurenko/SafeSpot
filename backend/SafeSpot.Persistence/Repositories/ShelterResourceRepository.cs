using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Domain.Enums;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class ShelterResourceRepository : Repository<ShelterResource>, IShelterResourceRepository
{
    private readonly ApplicationDbContext _db;

    public ShelterResourceRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _db.ShelterResources.AnyAsync(x => x.Id == id);
    }

    public async Task<bool> ExistsByTypeAsync(long shelterId, ResourceType type)
    {
        return await _db.ShelterResources.AnyAsync(x => x.ShelterId == shelterId && x.Type == type);
    }

    public async Task<List<ShelterResource>> GetAllByShelterIdAsync(long shelterId)
    {
        return await _db.ShelterResources.Where(x => x.ShelterId == shelterId).ToListAsync();
    }
}