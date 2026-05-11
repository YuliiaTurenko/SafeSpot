using SafeSpot.Domain.Entities;

namespace SafeSpot.Application.Abstractions;

public interface IShelterRepository : IRepository<Shelter>
{
    public Task<bool> ExistsByIdAsync(long id);
}
