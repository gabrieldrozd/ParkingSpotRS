using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Application.Exceptions;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Application.Commands.Handlers;

public class DeleteReservationCommandHandler : ICommandHandler<DeleteReservationCommand>
{
    private readonly IWeeklyParkingSpotRepository _repository;

    public DeleteReservationCommandHandler(IWeeklyParkingSpotRepository repository)
    {
        _repository = repository;
    }
    
    public async Task HandleAsync(DeleteReservationCommand command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);

        if (weeklyParkingSpot is null)
            throw new WeeklyParkingSpotNotFoundException();

        weeklyParkingSpot.RemoveReservation(command.ReservationId);
        await _repository.UpdateAsync(weeklyParkingSpot);
    }
    
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
        => (await _repository.GetAllAsync())
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
}