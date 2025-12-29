using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> entity)
        {
            entity.HasKey(r => r.Id);

            entity.Property(r => r.ReservationCode)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(r => r.SeatNumber)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(r => r.TotalPrice)
                  .HasColumnType("decimal(18,2)");

            entity.HasIndex(r => r.ReservationCode).IsUnique();

            entity.HasOne(r => r.Payment)
                  .WithOne(p => p.Reservation)
                  .HasForeignKey<Payment>(p => p.ReservationId)
                  .OnDelete(DeleteBehavior.Cascade);

            
        }
    }
}
