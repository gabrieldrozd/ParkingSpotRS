using ParkingSpotRS.Application.Abstractions;

namespace ParkingSpotRS.Application.Commands;

public sealed record DeleteReservationCommand(Guid ReservationId) : ICommand;