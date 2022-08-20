using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.DTOs;

namespace ParkingSpotRS.Application.Services;

public interface IReservationsService
{
    IEnumerable<ReservationDto> GetAllWeekly();
    ReservationDto Get(Guid id);
    Guid? Create(CreateReservation command);
    bool Update(ChangeReservationLicensePlate command);
    bool Delete(DeleteReservation command);
}