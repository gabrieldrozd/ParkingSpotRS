using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.Services;
using ParkingSpotRS.Core.Abstractions;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Infrastructure.DAL.Repositories;
using ParkingSpotRS.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace ParkingSpotRS.Tests.Unit.Services;

public class ReservationsServiceTests
{
    [Fact]
    public async Task given_valid_command_create_should_add_reservation()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"), 
            Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = await _reservationsService.CreateAsync(command);

        // Assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }
    
    [Fact]
    public async Task given_invalid_parking_spot_id_create_should_fail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000010"), 
            Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = await _reservationsService.CreateAsync(command);

        // Assert
        reservationId.ShouldBeNull();
    }
    
    [Fact]
    public async Task given_reservation_for_already_taken_date_create_should_fail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"), 
            Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));
        await _reservationsService.CreateAsync(command);

        // Act
        var reservationId = await _reservationsService.CreateAsync(command);

        // Assert
        reservationId.ShouldBeNull();
    }
    
    #region Arrange

    private readonly ReservationsService _reservationsService;

    public ReservationsServiceTests()
    {
        IClock clock = new TestClock();
        IWeeklyParkingSpotRepository weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(clock);
        _reservationsService = new ReservationsService(clock, weeklyParkingSpotRepository);
    }

    #endregion
}