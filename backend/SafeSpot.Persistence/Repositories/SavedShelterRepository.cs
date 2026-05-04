using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class SavedShelterRepository : Repository<SavedShelter>, ISavedShelterRepository
{
    public SavedShelterRepository(DbContext context) : base(context) { }
}
