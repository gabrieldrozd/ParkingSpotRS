﻿using ParkingSpotRS.Core.Exceptions;

namespace ParkingSpotRS.Core.ValueObjects;

public sealed record ReservationId
{
    public Guid Value { get; }

    public ReservationId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new InvalidEntityIdException(value);
        }
        
        Value = value;
    }

    public static ParkingSpotId Create() => new(Guid.NewGuid());

    public static implicit operator Guid(ReservationId id)
        => id.Value;
    
    public static implicit operator ReservationId(Guid value)
        => new(value);
}