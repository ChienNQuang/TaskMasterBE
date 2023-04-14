using Microsoft.EntityFrameworkCore;

namespace TaskMaster.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder)
    where TContext : DbContext
    {
        using var scope = host.Services.CreateScope();

        var services = scope.ServiceProvider;
        var logger = services.GetRequiredService<ILogger<TContext>>();
        var context = services.GetService<TContext>();
        

        try
        {
            context?.Database.EnsureCreated();
            logger.LogInformation("Migrating database..");
            ExecuteMigrations(context);
            logger.LogInformation("Migrated successfully");
            InvokeSeeder(seeder!, context, services);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured while migrating database!");
        }

        return host;
    }

    private static void ExecuteMigrations<TContext>(TContext context)
    where TContext : DbContext?
    {
        context?.Database.Migrate();
    }

    private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context,
        IServiceProvider services) 
    where TContext : DbContext?
    {
        seeder(context, services);
    }
}