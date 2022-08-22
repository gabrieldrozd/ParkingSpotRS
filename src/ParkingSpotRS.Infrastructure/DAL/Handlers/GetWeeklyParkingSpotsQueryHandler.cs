using Microsoft.EntityFrameworkCore;
using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Application.DTOs;
using ParkingSpotRS.Application.Queries;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Infrastructure.DAL.Handlers;

internal sealed class GetWeeklyParkingSpotsQueryHandler
    : IQueryHandler<GetWeeklyParkingSpotsQuery, IEnumerable<WeeklyParkingSpotDto>>
{
    private readonly DatabaseContext _context;

    public GetWeeklyParkingSpotsQueryHandler(DatabaseContext context)
        => _context = context;

    public async Task<IEnumerable<WeeklyParkingSpotDto>> HandleAsync(GetWeeklyParkingSpotsQuery query)
    {
        var week = query.Date.HasValue ? new Week(query.Date.Value) : null;
        var weeklyParkingSpots = await _context.WeeklyParkingSpots
            .Where(x => week == null || x.Week == week)
            .Include(x => x.Reservations)
            .AsNoTracking()
            .ToListAsync();

        return weeklyParkingSpots.Select(x => x.AsDto());
    }
}