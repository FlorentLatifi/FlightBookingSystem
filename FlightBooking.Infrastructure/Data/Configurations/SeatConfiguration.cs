using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class SeatConfiguration : IEntityTypeConfiguration<Seat>
    {
        public void Configure(EntityTypeBuilder<Seat> entity)
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.SeatNumberValue)
                  .IsRequired();

            entity.Property(s => s.IsAvailable)
                  .IsRequired();

            entity.Property(s => s.Class)
                  .IsRequired();

            entity.HasOne(s => s.Booking)
                  .WithMany(b => b.Seats)
                  .HasForeignKey(s => s.BookingId)
                  .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(s => s.Flight)
                  .WithMany(f => f.Seats)
                  .HasForeignKey(s => s.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
