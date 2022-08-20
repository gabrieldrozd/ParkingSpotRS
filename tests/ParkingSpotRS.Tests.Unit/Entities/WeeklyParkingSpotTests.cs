using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Exceptions;
using ParkingSpotRS.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace ParkingSpotRS.Tests.Unit.Entities;

public class WeeklyParkingSpotTests
{
    [Theory]
    [InlineData("2020-08-08")]
    [InlineData("2025-08-30")]
    [InlineData("2022-08-15")]
    public void given_invalid_date_add_reservation_should_fail(string dateString)
    {
        var invalidDate = DateTime.Parse(dateString);

        // Arrange
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", new Date(invalidDate));

        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, _now));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidReservationDateException>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    public void given_invalid_parking_spot_name_new_reservation_should_fail(string dateString)
    {
        // Arrange

        // Act
        var exception = Record.Exception(() => new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), dateString));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidParkingSpotNameException>();
    }

    [Fact]
    public void given_reservation_for_already_existing_date_add_reservation_should_fail()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Act
        var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(reservation, reservationDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<ParkingSpotAlreadyReservedException>();
    }
    
    [Fact]
    public void given_reservation_with_empty_guid_for_not_taken_date_add_reservation_should_fail()
    {
        // Arrange
        var emptyGuid = Guid.Empty;
        var reservationDate = _now.AddDays(1);

        // Act
        var exception = Record.Exception(() => new Reservation(emptyGuid, "Joe Doe", "RRS33816", reservationDate));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidEntityIdException>();
    }
    
    [Fact]
    public void given_reservation_id_for_non_existing_reservation_weeklyParkingSpot_reservations_should_not_be_empty()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Act
        _weeklyParkingSpot.RemoveReservation(Guid.NewGuid());

        // Assert
        _weeklyParkingSpot.Reservations.ShouldNotBeNull();
        _weeklyParkingSpot.Reservations.ShouldContain(reservation);
    }
    
    [Fact]
    public void given_reservation_id_for_existing_reservation_remove_reservation_should_succeed()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", reservationDate);
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Act
        _weeklyParkingSpot.RemoveReservation(reservation.Id);

        // Assert
        _weeklyParkingSpot.Reservations.ShouldNotContain(reservation);
        _weeklyParkingSpot.Reservations.ShouldBeEmpty();
    }
    
    [Fact]
    public void given_reservation_for_not_taken_date_add_reservation_should_succeed()
    {
        // Arrange
        var reservationDate = _now.AddDays(1);
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", reservationDate);

        // Act
        _weeklyParkingSpot.AddReservation(reservation, reservationDate);

        // Assert
        _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();
        _weeklyParkingSpot.Reservations.ShouldContain(reservation);
    }

    #region Arrange

    private readonly Date _now;
    private readonly WeeklyParkingSpot _weeklyParkingSpot;

    public WeeklyParkingSpotTests()
    {
        _now = new Date(DateTime.Parse("2022-08-16"));
        _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
    }

    #endregion
}