using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface IUserRepository : IRepository<User>
{
    public Task<User?> GetByIdentityIdAsync(string identityId);
    public Task<long> GetUserIdByIdentityIdAsync(string identityId);
    public Task<bool> ExistsByIdAsync(long Id);
}