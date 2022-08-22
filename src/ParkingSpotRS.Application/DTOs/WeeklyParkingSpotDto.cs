namespace ParkingSpotRS.Application.DTOs;

public class WeeklyParkingSpotDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int Capacity { get; set; }
    public IEnumerable<ReservationDto> Reservations { get; set; }
}