using Serilog;
using TaskMaster.Extensions;
using TaskMaster.Persistence;
var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

try
{
    builder.Host.UseSerilog();
    builder.Services.AddInfrastructure(builder.Configuration);
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
    var error = ex.GetType().Name;
    if (error.Equals("HostAbortedException", StringComparison.Ordinal))
    {
        throw;
    }
        
    Log.Fatal($"Unhandled exception1: {ex}");

}
finally
{
    Log.CloseAndFlush();
}
