using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly ApplicationDbContext _db;

    public UserRepository(ApplicationDbContext context) : base(context) 
    {
        _db = context;
    }

    public async Task<User?> GetByIdentityIdAsync(string identityId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }

    public async Task<long> GetUserIdByIdentityIdAsync(string identityId)
    {
        return await _db.Users
            .Where(x => x.IdentityId == identityId)
            .Select(x => x.Id)
            .FirstOrDefaultAsync();
    }

    public async Task<bool> ExistsByIdAsync(long Id)
    {
        return await _db.Users.AnyAsync(x => x.Id == Id);
    }
}
