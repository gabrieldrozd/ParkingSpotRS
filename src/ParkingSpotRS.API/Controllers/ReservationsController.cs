using Microsoft.AspNetCore.Mvc;
using ParkingSpotRS.Application.Commands;
using ParkingSpotRS.Application.DTOs;
using ParkingSpotRS.Application.Services;

namespace ParkingSpotRS.API.Controllers;

[ApiController]
[Route("reservations")]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsService _reservationsService;

    public ReservationsController(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }

    [HttpGet]
    public ActionResult<ReservationDto[]> Get() => Ok(_reservationsService.GetAllWeekly());

    [HttpGet("{id:guid}")]
    public ActionResult<ReservationDto> Get(Guid id)
    {
        var reservation = _reservationsService.Get(id);

        if (reservation is null) return NotFound();

        return reservation;
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {
        var id = _reservationsService.Create(command with {ReservationId = Guid.NewGuid()});

        if (id is null) return BadRequest();

        return CreatedAtAction(nameof(Get), new {Id = id}, default);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
    {
        var succeeded = _reservationsService.Update(command with {ReservationId = id});

        if (!succeeded) return BadRequest();

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        var succeeded = _reservationsService.Delete(new DeleteReservation(id));

        if (!succeeded) return BadRequest();

        return NoContent();
    }
}