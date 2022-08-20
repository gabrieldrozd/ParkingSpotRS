using Microsoft.EntityFrameworkCore;
using ParkingSpotRS.Core.Entities;

namespace ParkingSpotRS.Infrastructure.DAL;

internal sealed class DatabaseContext : DbContext
{
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<WeeklyParkingSpot> WeeklyParkingSpots { get; set; }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}