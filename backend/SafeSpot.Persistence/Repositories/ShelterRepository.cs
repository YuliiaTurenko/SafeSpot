using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class ShelterRepository : Repository<Shelter>, IShelterRepository
{
    public ShelterRepository(ApplicationDbContext context) : base(context) { }
}
