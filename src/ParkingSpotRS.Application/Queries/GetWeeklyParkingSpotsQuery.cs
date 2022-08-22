using ParkingSpotRS.Application.Abstractions;
using ParkingSpotRS.Application.DTOs;

namespace ParkingSpotRS.Application.Queries;

public class GetWeeklyParkingSpotsQuery : IQuery<IEnumerable<WeeklyParkingSpotDto>>
{
    public DateTime? Date { get; set; }
    
}