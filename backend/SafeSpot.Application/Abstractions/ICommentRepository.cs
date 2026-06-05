using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface ICommentRepository : IRepository<Comment>
{
    Task<List<Comment>> GetByPostIdAsync(long postId);
}
