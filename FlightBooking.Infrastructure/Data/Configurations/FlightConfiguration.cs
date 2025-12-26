using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class FlightConfiguration : IEntityTypeConfiguration<Flight>
    {
        public void Configure(EntityTypeBuilder<Flight> entity)
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.FlightNumber)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(f => f.BasePriceAmount)
                  .HasColumnType("decimal(18,2)");

            entity.Property(f => f.BasePriceCurrency)
                  .IsRequired()
                  .HasMaxLength(3);

            // Legacy string fields
            entity.Property(f => f.Airline).HasMaxLength(100);
            entity.Property(f => f.DepartureAirport).HasMaxLength(100);
            entity.Property(f => f.ArrivalAirport).HasMaxLength(100);
            entity.Property(f => f.Origin).HasMaxLength(100);
            entity.Property(f => f.Destination).HasMaxLength(100);

            entity.HasIndex(f => f.FlightNumber);
            entity.HasIndex(f => new { f.DepartureAirportId, f.ArrivalAirportId, f.DepartureTime });

            entity.HasOne(f => f.DepartureAirportEntity)
                  .WithMany(a => a.DepartureFlights)
                  .HasForeignKey(f => f.DepartureAirportId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.ArrivalAirportEntity)
                  .WithMany(a => a.ArrivalFlights)
                  .HasForeignKey(f => f.ArrivalAirportId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(f => f.AirlineEntity)
                  .WithMany(a => a.Flights)
                  .HasForeignKey(f => f.AirlineId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(f => f.Reservations)
                  .WithOne(r => r.Flight)
                  .HasForeignKey(r => r.FlightId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasMany(f => f.Bookings)
                  .WithOne(b => b.Flight)
                  .HasForeignKey(b => b.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasMany(f => f.Seats)
                  .WithOne(s => s.Flight)
                  .HasForeignKey(s => s.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

