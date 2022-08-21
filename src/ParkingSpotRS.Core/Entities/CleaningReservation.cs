using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Core.Entities;

public class CleaningReservation : Reservation
{
    public CleaningReservation(ReservationId id, Date date) : base(id, 2, date)
    {
    }
}