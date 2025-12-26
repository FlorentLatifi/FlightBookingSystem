using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.FirstName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.LastName)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.Email)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(p => p.PhoneNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(p => p.PassportNumber)
                  .IsRequired()
                  .HasMaxLength(20);

            entity.Property(p => p.Nationality)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.HasIndex(p => p.Email).IsUnique();
            entity.HasIndex(p => p.PassportNumber).IsUnique();

            entity.HasMany(p => p.Reservations)
                  .WithOne(r => r.Passenger)
                  .HasForeignKey(r => r.PassengerId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

