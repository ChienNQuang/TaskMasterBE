using Microsoft.EntityFrameworkCore;
using TaskMaster.Persistence;
using TaskMaster.MappingProfiles;
using TaskMaster.Middlewares;
using TaskMaster.Repositories;
using TaskMaster.Services;
using TaskMaster.Shared;

namespace TaskMaster.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureTaskMasterDbContext(configuration);
        services.AddServices();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UserMappingProfile>();
            config.AddProfile<ProjectMappingProfile>();
        });
        return services;
    }

    private static IServiceCollection ConfigureTaskMasterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        if (databaseSettings is null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
            throw new ArgumentNullException("Connection string is not configured");
        
        services.AddDbContext<TaskMasterContext>(b =>
        {
            b.UseNpgsql(databaseSettings.ConnectionString,
                o => o.UseNodaTime());
        });

        return services;
    }

    private static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ExceptionMiddleware>()
            .AddScoped<DbContext, TaskMasterContext>()
            .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
            .AddScoped<IUnitOfWork, UnitOfWork>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<IProjectService, ProjectService>();

        return services;
    }
}