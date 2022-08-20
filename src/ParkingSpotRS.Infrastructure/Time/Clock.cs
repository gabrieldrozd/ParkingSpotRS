using ParkingSpotRS.Application.Services;

namespace ParkingSpotRS.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}