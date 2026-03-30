using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Infrastructure.Identity;
using SafeSpot.Infrastructure.Services;
using SafeSpot.Persistence.Application;
using SafeSpot.Persistence.Identity;

namespace SafeSpot.Api.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddProjectServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Persistence")));

        services.AddDbContext<IdentityDbContext>(options => 
            options.UseSqlServer(connectionString, b => b.MigrationsAssembly("Persistence")));

        services.AddIdentity<ApplicationUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedEmail = true;
        }).AddEntityFrameworkStores<IdentityDbContext>().AddDefaultTokenProviders();

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }
}
