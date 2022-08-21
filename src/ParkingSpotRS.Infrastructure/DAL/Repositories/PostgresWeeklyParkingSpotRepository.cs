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

    public async Task<IEnumerable<WeeklyParkingSpot>> GetAllAsync()
        => await _weeklyParkingSpots
            .Include(x => x.Reservations)
            .ToListAsync();

    public async Task<IEnumerable<WeeklyParkingSpot>> GetByWeekAsync(Week week)
        => await _weeklyParkingSpots
            .Include(x => x.Reservations)
            .Where(x => x.Week == week)
            .ToListAsync();

    public async Task<WeeklyParkingSpot> GetAsync(ParkingSpotId id) 
        => await _weeklyParkingSpots
            .Include(x => x.Reservations)
            .SingleOrDefaultAsync(x => x.Id == id);

    public async Task AddAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        await _weeklyParkingSpots.AddAsync(weeklyParkingSpot);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(WeeklyParkingSpot weeklyParkingSpot)
    {
        _weeklyParkingSpots.Update(weeklyParkingSpot);
        await _context.SaveChangesAsync();
    }
}