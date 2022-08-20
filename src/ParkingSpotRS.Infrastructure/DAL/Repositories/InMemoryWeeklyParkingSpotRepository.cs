using ParkingSpotRS.Application.Services;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Infrastructure.Repositories;

internal sealed class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _weeklyParkingSpots = new List<WeeklyParkingSpot>
        {
            new(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
            new(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5"),
        };
    }

    public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;

    public IEnumerable<WeeklyParkingSpot> GetByWeek(Week week)
    {
        throw new NotImplementedException();
    }

    public WeeklyParkingSpot Get(ParkingSpotId id) => _weeklyParkingSpots.SingleOrDefault(x => x.Id == id);

    public void Add(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
    }

    public void Update(WeeklyParkingSpot weeklyParkingSpot)
    {
    }
}