using ParkingSpotRS.Application.Abstractions;

namespace ParkingSpotRS.Application.Commands;

public sealed record ReserveParkingSpotForVehicleCommand(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, 
    string LicensePlate, int Capacity, DateTime Date) : ICommand;