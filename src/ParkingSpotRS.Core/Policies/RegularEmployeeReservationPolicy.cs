﻿using ParkingSpotRS.Core.Abstractions;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Core.Policies;

internal sealed class RegularEmployeeReservationPolicy : IReservationPolicy
{
    private readonly IClock _clock;

    public RegularEmployeeReservationPolicy(IClock clock)
        => _clock = clock;

    public bool CanBeApplied(JobTitle jobTitle)
        => jobTitle == JobTitle.Employee;

    public bool CanReserve(IEnumerable<WeeklyParkingSpot> weeklyParkingSpots, EmployeeName employeeName)
    {
        var totalEmployeeReservations = weeklyParkingSpots
            .SelectMany(x => x.Reservations)
            .Count(x => x.EmployeeName == employeeName);

        return totalEmployeeReservations <= 2 && _clock.Current().Hour > 4;
    }
}