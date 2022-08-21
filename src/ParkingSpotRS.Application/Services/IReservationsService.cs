using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.DTOs;

namespace ParkingSpotRS.Application.Services;

public interface IReservationsService
{
    Task<IEnumerable<ReservationDto>> GetAllWeeklyAsync();
    Task<ReservationDto> GetAsync(Guid id);
    Task CreateAsync(CreateReservation command);
    Task UpdateAsync(ChangeReservationLicensePlate command);
    Task DeleteAsync(DeleteReservation command);
}