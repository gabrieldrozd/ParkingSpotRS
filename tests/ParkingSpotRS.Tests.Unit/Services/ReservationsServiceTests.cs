using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.Services;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Infrastructure.Repositories;
using ParkingSpotRS.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace ParkingSpotRS.Tests.Unit.Services;

public class ReservationsServiceTests
{
    [Fact]
    public void given_valid_command_create_should_add_reservation()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"), 
            Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = _reservationsService.Create(command);

        // Assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);
    }
    
    [Fact]
    public void given_invalid_parking_spot_id_create_should_fail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000010"), 
            Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));

        // Act
        var reservationId = _reservationsService.Create(command);

        // Assert
        reservationId.ShouldBeNull();
    }
    
    [Fact]
    public void given_reservation_for_already_taken_date_create_should_fail()
    {
        // Arrange
        var command = new CreateReservation(Guid.Parse("00000000-0000-0000-0000-000000000001"), 
            Guid.NewGuid(), "John Doe", "XYZ123", DateTime.UtcNow.AddDays(1));
        _reservationsService.Create(command);

        // Act
        var reservationId = _reservationsService.Create(command);

        // Assert
        reservationId.ShouldBeNull();
        
        //TODO: 11:48 koniec
    }
    
    #region Arrange

    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;
    private readonly ReservationsService _reservationsService;

    public ReservationsServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationsService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}