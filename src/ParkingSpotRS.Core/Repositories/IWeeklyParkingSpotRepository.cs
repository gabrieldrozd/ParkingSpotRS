using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Core.Repositories;

public interface IWeeklyParkingSpotRepository
{
    IEnumerable<WeeklyParkingSpot> GetAll();
    IEnumerable<WeeklyParkingSpot> GetByWeek(Week week) => throw new NotImplementedException();
    WeeklyParkingSpot Get(ParkingSpotId id);
    void Add(WeeklyParkingSpot weeklyParkingSpot);
    void Update(WeeklyParkingSpot weeklyParkingSpot);
}