using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class ShelterRepository : Repository<Shelter>, IShelterRepository
{
    public ShelterRepository(DbContext context) : base(context) { }
}
