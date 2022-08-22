using ParkingSpotRS.Application.Abstractions;

namespace ParkingSpotRS.Application.Commands;

public sealed record ReserveParkingSpotForCleaningCommand(DateTime Date) : ICommand;