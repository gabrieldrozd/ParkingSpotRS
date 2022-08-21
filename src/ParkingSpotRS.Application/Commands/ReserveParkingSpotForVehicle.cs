namespace ParkingSpotRS.Application.Commands;

public sealed record ReserveParkingSpotForVehicle(Guid ParkingSpotId, Guid ReservationId, string EmployeeName, 
    string LicensePlate, int Capacity, DateTime Date);