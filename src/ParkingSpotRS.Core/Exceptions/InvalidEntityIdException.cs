namespace ParkingSpotRS.Core.Exceptions;

public class InvalidEntityIdException : CustomException
{
    public InvalidEntityIdException(Guid id)
        : base($"Id: {id} is invalid")
    {
    }
}