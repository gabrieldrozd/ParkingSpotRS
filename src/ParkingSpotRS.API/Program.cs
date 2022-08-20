using ParkingSpotRS.Application;
using ParkingSpotRS.Application.Services;
using ParkingSpotRS.Core;
using ParkingSpotRS.Core.Repositories;
using ParkingSpotRS.Infrastructure;
using ParkingSpotRS.Infrastructure.Repositories;
using ParkingSpotRS.Infrastructure.Time;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration)
    .AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();