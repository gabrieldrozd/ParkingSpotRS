using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.DTOs;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Exceptions;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Application.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationsService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }

    public IEnumerable<ReservationDto> GetAllWeekly()
        => _weeklyParkingSpotRepository
            .GetAll()
            .SelectMany(x => x.Reservations)
            .Select(x => new ReservationDto
            {
                Id = x.Id,
                EmployeeName = x.EmployeeName,
                Date = x.Date.Value.Date
            });

    public ReservationDto Get(Guid id)
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public Guid? Create(CreateReservation command)
    {
        try
        {
            var (spotId, reservationId, employeeName, licensePlate, date) = command;
            var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(spotId);

            if (weeklyParkingSpot is null)
                return default;

            var reservation = new Reservation(reservationId, employeeName, licensePlate, new Date(date));

            weeklyParkingSpot.AddReservation(reservation, new Date(CurrentDate()));
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return reservation.Id;
        }
        catch (CustomException)
        {
            return default;
        }
    }

    public bool Update(ChangeReservationLicensePlate command)
    {
        try
        {
            var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

            if (weeklyParkingSpot is null)
            {
                return false;
            }

            var reservationId = new ReservationId(command.ReservationId);
            var reservation = weeklyParkingSpot.Reservations
                .SingleOrDefault(x => x.Id == reservationId);

            if (reservation is null)
                return false;

            reservation.ChangeLicensePlate(command.LicensePlate);
            _weeklyParkingSpotRepository.Update(weeklyParkingSpot);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool Delete(DeleteReservation command)
    {
        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);

        if (weeklyParkingSpot is null)
            return false;

        return true;
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId id)
        => _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(r => r.Id == id));

    private DateTime CurrentDate() => _clock.Current();
}