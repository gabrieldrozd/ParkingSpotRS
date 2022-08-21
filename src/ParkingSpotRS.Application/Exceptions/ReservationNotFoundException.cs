using ParkingSpotRS.Core.Exceptions;

namespace ParkingSpotRS.Application.Exceptions;

public sealed class ReservationNotFoundException : CustomException
{
    public ReservationNotFoundException(Guid id) 
        : base($"Reservation with ID: {id} was not found")
    {
    }
}