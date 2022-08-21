﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSpotRS.Core.Entities;
using ParkingSpotRS.Core.ValueObjects;

namespace ParkingSpotRS.Infrastructure.DAL.Configurations;

internal sealed class VehicleReservationConfiguration : IEntityTypeConfiguration<VehicleReservation>
{
    public void Configure(EntityTypeBuilder<VehicleReservation> builder)
    {
        builder.Property(x => x.EmployeeName)
            .IsRequired()
            .HasConversion(x => x.Value, x => new EmployeeName(x));
        
        builder.Property(x => x.LicensePlate)
            .IsRequired()
            .HasConversion(x => x.Value, x => new LicensePlate(x));
    }
}