using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;
using SafeSpot.Persistence.Application;

namespace SafeSpot.Persistence.Repositories;

public class UserSettingsRepository : Repository<UserSettings>, IUserSettingsRepository
{
    public UserSettingsRepository(ApplicationDbContext context) : base(context) { }
}