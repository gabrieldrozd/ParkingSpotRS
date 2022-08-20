using Microsoft.EntityFrameworkCore;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Infrastructure.DAL.Repositories;

internal sealed class PostgresWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly DatabaseContext _context;
    private readonly DbSet<WeeklyParkingSpot> _weeklyParkingSpots;

    public PostgresWeeklyParkingSpotRepository(DatabaseContext context)
    {
        _context = context;
        _weeklyParkingSpots = _context.WeeklyParkingSpots;
    }

    public IEnumerable<WeeklyParkingSpot> GetAll()
        => _weeklyParkingSpots
            .Include(x => x.Reservations)
            .ToList();

    public IEnumerable<WeeklyParkingSpot> GetByWeek(Week week)
        => _weeklyParkingSpots
            .Include(x => x.Reservations)
            .Where(x => x.Week == week)
            .ToList();

    public WeeklyParkingSpot Get(ParkingSpotId id) 
        => _weeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefault(x => x.Id == id);

    public void Add(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Add(weeklyParkingSpot);
        _context.SaveChanges();
    }

    public void Update(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Update(weeklyParkingSpot);
        _context.SaveChanges();
    }
}