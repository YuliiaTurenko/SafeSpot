using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class ShelterResourceRepository : Repository<ShelterResource>, IShelterResourceRepository
{
    public ShelterResourceRepository(ApplicationDbContext context) : base(context) { }
}