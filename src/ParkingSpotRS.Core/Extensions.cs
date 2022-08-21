using Microsoft.Extensions.DependencyInjection;
using ParkingSpotRS.Core.DomainServices;
using ParkingSpotRS.Core.Policies;

namespace ParkingSpotRS.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddSingleton<IReservationPolicy, RegularEmployeeReservationPolicy>();
        services.AddSingleton<IReservationPolicy, ManagerReservationPolicy>();
        services.AddSingleton<IReservationPolicy, BossReservationPolicy>();
        services.AddSingleton<IParkingReservationService, ParkingReservationService>();
        
        return services;
    }
}