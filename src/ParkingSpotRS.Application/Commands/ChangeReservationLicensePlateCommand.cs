using ParkingSpotRS.Application.Abstractions;

namespace ParkingSpotRS.Application.Commands;

public sealed record ChangeReservationLicensePlateCommand(Guid ReservationId, string LicensePlate) : ICommand;