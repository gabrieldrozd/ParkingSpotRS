using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Core.Abstractions;
using ParkingSpotRS.Infrastructure.DAL;
using ParkingSpotRS.Infrastructure.Exceptions;
using ParkingSpotRS.Infrastructure.Time;

[assembly: InternalsVisibleTo("ParkingSpotRS.Tests.Unit")]
namespace ParkingSpotRS.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.AddControllers();
        services.Configure<AppOptions>(config.GetRequiredSection("app"));
        services.AddSingleton<ExceptionMiddleware>();
        
        services
            .AddDatabase(config)
            // .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
            .AddSingleton<IClock, Clock>();
        
        var infrastructureAssembly = typeof(AppOptions).Assembly;
        services.Scan(s => s.FromAssemblies(infrastructureAssembly)
            .AddClasses(c => c.AssignableTo(typeof(IQueryHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

        return services;
    }

    public static WebApplication UseInfrastructure(this WebApplication app)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        app.MapControllers();
        
        return app;
    }

    public static T GetOptions<T>(this IConfiguration config, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = config.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}