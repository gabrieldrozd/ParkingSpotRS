using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ParkingSpotRS.Core.Entities;

namespace ParkingSpotRS.Infrastructure.DAL.Configurations;

internal sealed class CleaningReservationConfiguration : IEntityTypeConfiguration<CleaningReservation>
{
    public void Configure(EntityTypeBuilder<CleaningReservation> builder)
    {
    }
}