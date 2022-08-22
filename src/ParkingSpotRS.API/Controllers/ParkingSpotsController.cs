using Microsoft.AspNetCore.Mvc;
using ParkingSpotRS.API.Controllers.Base;
using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.DTOs;
using ParkingSpotRS.Application.Queries;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.API.Controllers;

public class ParkingSpotsController : BaseApiController
{
    private readonly ICommandHandler<ReserveParkingSpotForVehicleCommand> _reserveParkingSpotForVehicleCommandHandler;
    private readonly ICommandHandler<ReserveParkingSpotForCleaningCommand> _reserveParkingSpotForCleaningCommandHandler;

    private readonly IQueryHandler<GetWeeklyParkingSpotsQuery, IEnumerable<WeeklyParkingSpotDto>>
        _getWeeklyParkingSpotsQueryHandler;

    public ParkingSpotsController(
        ICommandHandler<ReserveParkingSpotForVehicleCommand> reserveParkingSpotForVehicleCommandHandler,
        ICommandHandler<ReserveParkingSpotForCleaningCommand> reserveParkingSpotForCleaningCommandHandler,
        IQueryHandler<GetWeeklyParkingSpotsQuery, IEnumerable<WeeklyParkingSpotDto>> getWeeklyParkingSpotsQueryHandler)
    {
        _reserveParkingSpotForVehicleCommandHandler = reserveParkingSpotForVehicleCommandHandler;
        _reserveParkingSpotForCleaningCommandHandler = reserveParkingSpotForCleaningCommandHandler;
        _getWeeklyParkingSpotsQueryHandler = getWeeklyParkingSpotsQueryHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<WeeklyParkingSpotDto>>> Get([FromQuery] GetWeeklyParkingSpotsQuery query)
        => Ok(await _getWeeklyParkingSpotsQueryHandler.HandleAsync(query));

    [HttpPost("{parkingSpotId:guid}/reservations/vehicle")]
    public async Task<ActionResult> Post(Guid parkingSpotId, [FromBody] ReserveParkingSpotForVehicleCommand command)
    {
        await _reserveParkingSpotForVehicleCommandHandler.HandleAsync(command with {ReservationId = parkingSpotId});
        return NoContent();
    }
    
    [HttpPost("reservations/cleaning")]
    public async Task<ActionResult> Post([FromBody] ReserveParkingSpotForCleaningCommand command)
    {
        await _reserveParkingSpotForCleaningCommandHandler.HandleAsync(command);
        return NoContent();
    }
}