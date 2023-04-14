using System.Data.Common;
using Microsoft.EntityFrameworkCore;
using TaskMaster.Persistence;
using TaskMaster.Repositories;
using TaskMaster.Shared;

namespace TaskMaster.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureTaskMasterDbContext(configuration);
        services.AddScoped<DbContext, TaskMasterContext>()
            .AddScoped(typeof(IRepository<,>), typeof(Repository<,>))
            .AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        return services;
    }

    private static IServiceCollection ConfigureTaskMasterDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        if (databaseSettings is null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
            throw new ArgumentNullException("Connection string is not configured");
        
        services.AddDbContext<TaskMasterContext>(b =>
        {
            b.UseNpgsql(databaseSettings.ConnectionString);
        });

        return services;
    }
}