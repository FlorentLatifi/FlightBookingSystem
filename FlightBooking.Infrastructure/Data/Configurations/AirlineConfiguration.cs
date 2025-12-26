using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class AirlineConfiguration : IEntityTypeConfiguration<Airline>
    {
        public void Configure(EntityTypeBuilder<Airline> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Code)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(a => a.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(a => a.Country)
                  .HasMaxLength(100);

            entity.Property(a => a.LogoUrl)
                  .HasMaxLength(500);

            entity.HasIndex(a => a.Code).IsUnique();
        }
    }
}

