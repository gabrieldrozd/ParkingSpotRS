using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ParkingSpotRS.Infrastructure;

namespace ParkingSpotRS.API.Controllers;

[Route("")]
public class HomeController : ControllerBase
{
    private readonly AppOptions _appOptions;

    public HomeController(IOptionsMonitor<AppOptions> appOptions)
    {
        _appOptions = appOptions.CurrentValue;
    }
    
    [HttpGet]
    public ActionResult Get() => Ok(_appOptions.Name);
}