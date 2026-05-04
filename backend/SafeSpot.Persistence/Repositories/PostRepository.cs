using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class PostRepository : Repository<Post>, IPostRepository
{
    public PostRepository(DbContext context) : base(context) { }
}