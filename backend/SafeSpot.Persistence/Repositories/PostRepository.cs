using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(ApplicationDbContext context) : base(context) { }
}