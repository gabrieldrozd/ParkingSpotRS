namespace ParkingSpotRS.Application.Commands;

public sealed record ChangeReservationLicensePlate(Guid ReservationId, string LicensePlate);