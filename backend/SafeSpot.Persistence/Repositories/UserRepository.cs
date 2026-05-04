using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Domain.Entities;

namespace SafeSpot.Persistence.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(DbContext context) : base(context) { }
}
