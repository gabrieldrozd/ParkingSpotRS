using ParkingSpotRS.Core.Abstractions;

namespace ParkingSpotRS.Tests.Unit.Shared;

public sealed class TestClock : IClock
{
    public DateTime Current() => new(2022, 08, 17);
}