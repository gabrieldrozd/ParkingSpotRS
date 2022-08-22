using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Core.DomainServices;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Application.Commands.Handlers;

public class ReserveParkingSpotForCleaningCommandHandler : ICommandHandler<ReserveParkingSpotForCleaningCommand>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _reservationService;

    public ReserveParkingSpotForCleaningCommandHandler(IWeeklyParkingSpotRepository repository,
        IParkingReservationService reservationService)
    {
        _repository = repository;
        _reservationService = reservationService;
    }

    public async Task HandleAsync(ReserveParkingSpotForCleaningCommand command)
    {
        var week = new Week(command.Date);
        var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();

        _reservationService.ReserveParkingForCleaning(weeklyParkingSpots, new Date(command.Date));

        foreach (var parkingSpot in weeklyParkingSpots)
        {
            await _repository.UpdateAsync(parkingSpot);
        }
    }
}