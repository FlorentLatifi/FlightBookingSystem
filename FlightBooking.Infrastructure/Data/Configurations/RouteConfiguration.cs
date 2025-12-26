using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class RouteConfiguration : IEntityTypeConfiguration<Route>
    {
        public void Configure(EntityTypeBuilder<Route> entity)
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.DistanceKm)
                  .HasColumnType("decimal(18,2)");

            entity.HasOne(r => r.DepartureAirport)
                  .WithMany()
                  .HasForeignKey(r => r.DepartureAirportId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(r => r.ArrivalAirport)
                  .WithMany()
                  .HasForeignKey(r => r.ArrivalAirportId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasIndex(r => new { r.DepartureAirportId, r.ArrivalAirportId })
                  .IsUnique();
        }
    }
}
