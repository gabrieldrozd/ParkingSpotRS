using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Infrastructure.DAL.Repositories;

namespace ParkingSpotRS.Infrastructure.DAL;

internal static class Extensions
{
    private const string OptionsSectionName = "postgres";
    
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<DatabaseOptions>(config.GetRequiredSection(OptionsSectionName));
        var databaseOptions = config.GetOptions<DatabaseOptions>(OptionsSectionName);
        services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(databaseOptions.ConnectionString));
        services.AddScoped<IWeeklyParkingSpotRepository, PostgresWeeklyParkingSpotRepository>();
        services.AddHostedService<DatabaseInitializer>();
        
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        return services;
    }
}