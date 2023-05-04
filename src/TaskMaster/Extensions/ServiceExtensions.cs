using System.Text;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TaskMaster.Persistence;
using TaskMaster.MappingProfiles;
using TaskMaster.Middlewares;
using TaskMaster.Policies;
using TaskMaster.Repositories;
using TaskMaster.Services;
using TaskMaster.Shared;
using TaskMaster.Validators;

namespace TaskMaster.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureTaskMasterDbContext(configuration);
        services.AddJwtAuthentication(configuration);
        services.AddAuthorization();
        services.AddServices();
        services.AddControllers(opt =>
            opt.Conventions.Add(new RouteTokenTransformerConvention(new SlugifyParameterTransformer())));
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddAutoMapper(config =>
        {
            config.AddProfile<UserMappingProfile>();
            config.AddProfile<ProjectMappingProfile>();
        });
        return services;
    }

    private static IServiceCollection ConfigureTaskMasterDbContext(this IServiceCollection services,
        IConfiguration configuration)
    {
        var databaseSettings = configuration.GetSection(nameof(DatabaseSettings)).Get<DatabaseSettings>();
        if (databaseSettings is null || string.IsNullOrEmpty(databaseSettings.ConnectionString))
            throw new InvalidOperationException("No connection strings are provided!");

        services.AddDbContext<TaskMasterContext>(b =>
        {
            b.UseNpgsql(databaseSettings.ConnectionString,
                o => o.UseNodaTime());
        });

        return services;
    }

    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = configuration.GetSection(nameof(JwtSettings)).Get<JwtSettings>();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true
            };
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