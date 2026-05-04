using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class ShelterResourceRepository : Repository<ShelterResource>, IShelterResourceRepository
{
    public ShelterResourceRepository(DbContext context) : base(context) { }
}