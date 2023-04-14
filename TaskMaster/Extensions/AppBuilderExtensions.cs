using Serilog;

namespace TaskMaster.Extensions;

public static class AppBuilderExtensions
{
    public static void AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog();
// Add services to the container.

        builder.Services.ConfigureServices(builder.Configuration);
    }   
}