using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class UserSettingsRepository : Repository<UserSettings>, IUserSettingsRepository
{
    public UserSettingsRepository(DbContext context) : base(context) { }
}