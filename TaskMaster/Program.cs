using System.Collections.Immutable;
using Serilog;
using TaskMaster.Extensions;
using TaskMaster.Persistence;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

try
{
    builder.AddInfrastructure();
    var app = builder.Build();
    app.UseInfrastructure();
    app.MigrateDatabase<TaskMasterContext>((context, _) =>
    {
        // TaskMasterContextSeed.SeedUserAsync(context, Log.Logger).Wait();
        // TaskMasterContextSeed.SeedProjectAsync(context, Log.Logger).Wait();
        // TaskMasterContextSeed.SeedTaskAsync(context, Log.Logger).Wait();
    }).Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "App closed unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
