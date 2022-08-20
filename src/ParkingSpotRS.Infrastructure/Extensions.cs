using System.Runtime.CompilerServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpotRS.Application.Services;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Infrastructure.DAL;
using ParkingSpotRS.Infrastructure.Repositories;
using ParkingSpotRS.Infrastructure.Time;

[assembly: InternalsVisibleTo("ParkingSpotRS.Tests.Unit")]
namespace ParkingSpotRS.Infrastructure;

public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<AppOptions>(config.GetRequiredSection("app"));
        
        services
            .AddDatabase(config)
            // .AddSingleton<IWeeklyParkingSpotRepository, InMemoryWeeklyParkingSpotRepository>()
            .AddSingleton<IClock, Clock>();

        return services;
    }

    public static T GetOptions<T>(this IConfiguration config, string sectionName) where T : class, new()
    {
        var options = new T();
        var section = config.GetRequiredSection(sectionName);
        section.Bind(options);

        return options;
    }
}