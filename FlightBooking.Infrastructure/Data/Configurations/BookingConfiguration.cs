using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> entity)
        {
            entity.HasKey(b => b.Id);

            entity.Property(b => b.BookingReference)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(b => b.PassengerName)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(b => b.PassengerEmail)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(b => b.PassengerPhone)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(b => b.TotalPriceAmount)
                  .HasColumnType("decimal(18,2)");

            entity.Property(b => b.TotalPriceCurrency)
                  .IsRequired()
                  .HasMaxLength(3);

            entity.HasIndex(b => b.BookingReference).IsUnique();

            entity.HasOne(b => b.Flight)
                  .WithMany(f => f.Bookings)
                  .HasForeignKey(b => b.FlightId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(b => b.Payment)
                  .WithOne(p => p.Booking)
                  .HasForeignKey<Payment>(p => p.BookingId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
