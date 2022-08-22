using ParkingSpotRS.Application.DTOs;
using ParkingSpotRS.Core.Entities;

namespace ParkingSpotRS.Infrastructure.DAL.Handlers;

public static class Extensions
{
    public static WeeklyParkingSpotDto AsDto(this WeeklyParkingSpot entity)
        => new()
        {
            Id = entity.Id.Value,
            Name = entity.Name.Value,
            Capacity = entity.Capacity.Value,
            From = entity.Week.From.Value.DateTime,
            To = entity.Week.To.Value.DateTime,
            Reservations = entity.Reservations.Select(x => new ReservationDto
            {
                Id = x.Id,
                EmployeeName = x is VehicleReservation vr ? vr.EmployeeName : string.Empty,
                Date = x.Date.Value.Date
            })
        };
}