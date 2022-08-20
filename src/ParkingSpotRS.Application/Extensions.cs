using Microsoft.Extensions.DependencyInjection;
using ParkingSpotRS.Application.Services;

namespace ParkingSpotRS.Application;

public static class Extensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IReservationsService, ReservationsService>();

        return services;
    }
}