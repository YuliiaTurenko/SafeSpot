using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class CommentRepository : Repository<Comment>, ICommentRepository
{
    public CommentRepository(DbContext context) : base(context) { }
}