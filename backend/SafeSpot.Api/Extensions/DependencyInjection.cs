using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SafeSpot.Application.Abstractions;
using SafeSpot.Application.Features.Announcements.Commands.Create;
using SafeSpot.Application.Features.Announcements.Commands.Delete;
using SafeSpot.Application.Features.Announcements.Commands.Update;
using SafeSpot.Application.Features.Sensors.Commands.Create;
using SafeSpot.Application.Features.Sensors.Commands.Delete;
using SafeSpot.Application.Features.Sensors.Commands.Update;
using SafeSpot.Application.Features.Sensors.Queries.GetById;
using SafeSpot.Application.Features.ShelterResources.Commands.Create;
using SafeSpot.Application.Features.ShelterResources.Commands.Delete;
using SafeSpot.Application.Features.ShelterResources.Commands.Update;
using SafeSpot.Application.Features.Shelters.Commands.Create;
using SafeSpot.Application.Features.Shelters.Commands.Delete;
using SafeSpot.Application.Features.Shelters.Commands.Update;
using SafeSpot.Application.Features.User.Commands.Create;
using SafeSpot.Application.Features.User.Commands.Update;
using SafeSpot.Infrastructure.Identity;
using SafeSpot.Infrastructure.IoT;
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

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly);
        });

        services.AddHttpContextAccessor();
        services.AddScoped<IUserContext, UserContext>();
        services.AddScoped<ILocalizationService, LocalizationService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<INotificationService, NotificationService>();
        services.AddScoped<AdminService>();
        services.AddSignalR();
        services.AddHostedService<MqttHostedService>();
        services.AddHostedService<SensorOfflineWatcherService>();
        
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

        services.AddScoped<IValidator<CreateShelterCommand>, CreateShelterCommandValidator>();
        services.AddScoped<IValidator<UpdateShelterCommand>, UpdateShelterCommandValidator>();
        services.AddScoped<IValidator<DeleteShelterCommand>, DeleteShelterCommandValidator>();

        services.AddScoped<IValidator<CreateResourceCommand>, CreateResourceCommandValidator>();
        services.AddScoped<IValidator<UpdateResourceCommand>, UpdateResourceCommandValidator>();
        services.AddScoped<IValidator<DeleteResourceCommand>, DeleteResourceCommandValidator>();

        services.AddScoped<IValidator<CreateAnnouncementCommand>, CreateAnnouncementCommandValidator>();
        services.AddScoped<IValidator<UpdateAnnouncementCommand>, UpdateAnnouncementCommandValidator>();
        services.AddScoped<IValidator<DeleteAnnouncementCommand>, DeleteAnnouncementCommandValidator>();

        services.AddScoped<IValidator<CreateSensorCommand>, CreateSensorCommandValidator>();
        services.AddScoped<IValidator<UpdateSensorCommand>, UpdateSensorCommandValidator>();
        services.AddScoped<IValidator<DeleteSensorCommand>, DeleteSensorCommandValidator>();
        services.AddScoped<IValidator<GetSensorByIdQuery>, GetSensorByIdQueryValidator>();

        return services;
    }
}
