using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Application.Exceptions;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Application.Commands.Handlers;

public class ChangeReservationLicensePlateCommandHandler : ICommandHandler<ChangeReservationLicensePlateCommand>
{
    private readonly IWeeklyParkingSpotRepository _repository;

    public ChangeReservationLicensePlateCommandHandler(IWeeklyParkingSpotRepository repository)
        => _repository = repository;

    public async Task HandleAsync(ChangeReservationLicensePlateCommand command)
    {
        var weeklyParkingSpot = await GetWeeklyParkingSpotByReservation(command.ReservationId);

        if (weeklyParkingSpot is null)
            throw new WeeklyParkingSpotNotFoundException();

        var reservationId = new ReservationId(command.ReservationId);
        var reservation = weeklyParkingSpot.Reservations
            .OfType<VehicleReservation>()
            .SingleOrDefault(x => x.Id == reservationId);

        if (reservation is null)
            throw new ReservationNotFoundException(command.ReservationId);

        reservation.ChangeLicensePlate(command.LicensePlate);
        await _repository.UpdateAsync(weeklyParkingSpot);
    }
    
    private async Task<WeeklyParkingSpot> GetWeeklyParkingSpotByReservation(ReservationId id)
        => (await _repository.GetAllAsync())
            .SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));
}