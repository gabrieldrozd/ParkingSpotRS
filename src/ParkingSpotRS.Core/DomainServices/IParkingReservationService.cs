using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Core.DomainServices;

public interface IParkingReservationService
{
    void ReserveSpotForVehicle(IEnumerable<WeeklyParkingSpot> allParkingSpots, JobTitle jobTitle,
        WeeklyParkingSpot parkingSpotToReserve, VehicleReservation reservation);

    void ReserveParkingForCleaning(IEnumerable<WeeklyParkingSpot> allParkingSpots, Date date);
}