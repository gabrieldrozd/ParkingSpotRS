using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Application.Exceptions;
using ParkingSpotRS.Core.Abstractions;
using ParkingSpotRS.Core.DomainServices;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Application.Commands.Handlers;

public sealed class ReserveParkingSpotForVehicleCommandHandler : ICommandHandler<ReserveParkingSpotForVehicleCommand>
{
    private readonly IWeeklyParkingSpotRepository _repository;
    private readonly IParkingReservationService _reservationService;
    private readonly IClock _clock;

    public ReserveParkingSpotForVehicleCommandHandler(IWeeklyParkingSpotRepository repository,
        IParkingReservationService reservationService, IClock clock)
    {
        _repository = repository;
        _reservationService = reservationService;
        _clock = clock;
    }

    public async Task HandleAsync(ReserveParkingSpotForVehicleCommand command)
    {
        var (spotId, reservationId, employeeName, licensePlate, capacity, date) = command;
        var week = new Week(_clock.Current());
        var parkingSpotId = new ParkingSpotId(spotId);
        var weeklyParkingSpots = (await _repository.GetByWeekAsync(week)).ToList();
        var parkingSpotToReserve = weeklyParkingSpots.SingleOrDefault(x => x.Id == parkingSpotId);
        

        if (parkingSpotToReserve is null)
            throw new WeeklyParkingSpotNotFoundException(spotId);

        var reservation = new VehicleReservation(reservationId, employeeName, licensePlate, capacity, new Date(date));

        _reservationService.ReserveSpotForVehicle(weeklyParkingSpots, JobTitle.Employee,
            parkingSpotToReserve, reservation);

        await _repository.UpdateAsync(parkingSpotToReserve);
    }
}