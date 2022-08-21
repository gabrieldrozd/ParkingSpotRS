using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Core.Exceptions;

public sealed class ParkingSpotCapacityExceededException : CustomException
{
    public ParkingSpotId ParkingSpotId { get; }

    public ParkingSpotCapacityExceededException(ParkingSpotId parkingSpotId)
        : base($"Parking spot with ID: {parkingSpotId} exceeded its capacity.")
    {
        ParkingSpotId = parkingSpotId;
    }
}