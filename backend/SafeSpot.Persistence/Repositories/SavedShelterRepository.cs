using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class SavedShelterRepository : Repository<SavedShelter>, ISavedShelterRepository
{
    public SavedShelterRepository(ApplicationDbContext context) : base(context) { }
}
