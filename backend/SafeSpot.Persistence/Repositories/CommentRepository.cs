using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    private readonly ApplicationDbContext _db;

    public CommentRepository(ApplicationDbContext context) : base(context)
    {
        _db = context;
    }

    public async Task<List<Comment>> GetByPostIdAsync(long postId)
    {
        return await _db.Comments
            .Include(c => c.User)
            .Where(c => c.PostId == postId)
            .OrderBy(c => c.CreatedAt)
            .ToListAsync();
    }
}