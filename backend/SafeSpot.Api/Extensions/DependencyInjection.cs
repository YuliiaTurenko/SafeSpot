using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.User.Commands.Create;
using SafeSpot.Application.Features.User.Commands.Update;
using SafeSpot.Infrastructure.Identity;
using SafeSpot.Infrastructure.Services;
using SafeSpot.Persistence.Application;
using SafeSpot.Persistence.Identity;
using SafeSpot.Persistence.Repositories;

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

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IShelterRepository, ShelterRepository>();
        services.AddScoped<ISensorRepository, SensorRepository>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        services.AddScoped<IPostRepository, PostRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ISensorReadingRepository, SensorReadingRepository>();
        services.AddScoped<IShelterResourceRepository, ShelterResourceRepository>();
        services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
        services.AddScoped<ISavedShelterRepository, SavedShelterRepository>();
        services.AddScoped<IAuditLogRepository, AuditLogRepository>();

        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
        services.AddScoped<IValidator<UpdateUserCommand>, UpdateUserCommandValidator>();

        return services;
    }
}
