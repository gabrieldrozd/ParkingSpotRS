using ParkingSpotRS.Application.Services;
using ParkingSpotRS.Core.Abstractions;

namespace ParkingSpotRS.Infrastructure.Time;

internal sealed class Clock : IClock
{
    public DateTime Current() => DateTime.UtcNow;
}