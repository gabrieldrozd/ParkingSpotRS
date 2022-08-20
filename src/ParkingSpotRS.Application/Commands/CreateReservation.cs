namespace ParkingSpotRS.Application.Commands;

public sealed record CreateReservation(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, 
    string LicensePlate, DateTime Date);