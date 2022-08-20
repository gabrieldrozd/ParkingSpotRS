using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.Exceptions;
using ParkingSpotRS.Core.ValueObjects;
using Shouldly;
using Xunit;

namespace ParkingSpotRS.Tests.Unit.Entities;

public class ReservationTests
{
    [Theory]
    [InlineData("XYZ")]
    [InlineData("XYZ123456789")]
    [InlineData("")]
    public void given_invalid_license_plate_change_license_plate_should_fail(string dateString)
    {
        // Arrange
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", _now.AddDays(1));
        
        // Act
        var exception = Record.Exception(() => reservation.ChangeLicensePlate(dateString));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidLicensePlateException>();
    }
    
    [Fact]
    public void given_license_plate_change_license_plate_should_succeed()
    {
        // Arrange
        var validLicensePlate = "RRS32137";
        var reservation = new Reservation(Guid.NewGuid(), "Joe Doe", "RRS33816", _now.AddDays(1));
        
        // Act
        reservation.ChangeLicensePlate(validLicensePlate);

        // Assert
        reservation.LicensePlate.Value.ShouldBe(validLicensePlate);
    }
    
    [Fact]
    public void given_invalid_employee_name_new_reservation_should_fail()
    {
        // Arrange
        var invalidEmployeeName = "";
        
        // Act
        var exception = Record.Exception(() =>
            new Reservation(Guid.NewGuid(), invalidEmployeeName, "RRS33816", _now.AddDays(1)));

        // Assert
        exception.ShouldNotBeNull();
        exception.ShouldBeOfType<InvalidEmployeeNameException>();
    }
    
    [Fact]
    public void given_employee_name_new_reservation_should_succeed()
    {
        // Arrange
        var employeeName = "John Doe";
        
        // Act
        var reservation = new Reservation(Guid.NewGuid(), employeeName, "RRS33816", _now.AddDays(1));

        // Assert
        reservation.EmployeeName.Value.ShouldBe(employeeName);
    }
    
    #region Arrange

    private readonly Date _now;

    public ReservationTests()
    {
        _now = new Date(DateTime.Parse("2022-08-16"));
    }

    #endregion
}