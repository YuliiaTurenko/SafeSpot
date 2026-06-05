using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    private readonly ApplicationDbContext _db;

    public PostRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<List<Post>> GetByShelterIdAsync(long shelterId)
    {
        return await _db.Posts
            .Include(p => p.User)
            .Include(p => p.Comments)
            .Where(p => p.ShelterId == shelterId)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }
}