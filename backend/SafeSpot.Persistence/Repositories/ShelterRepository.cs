using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;
using Microsoft.EntityFrameworkCore;

namespace SafeSpot.Persistence.Repositories;

public class ShelterRepository : Repository<Shelter>, IShelterRepository
{
    private readonly ApplicationDbContext _db;

    public ShelterRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<bool> ExistsByIdAsync(long id)
    {
        return await _db.Shelters.AnyAsync(x => x.Id == id);
    }
}
