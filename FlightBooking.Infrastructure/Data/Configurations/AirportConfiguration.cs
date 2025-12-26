using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class AirportConfiguration : IEntityTypeConfiguration<Airport>
    {
        public void Configure(EntityTypeBuilder<Airport> entity)
        {
            entity.HasKey(a => a.Id);

            entity.Property(a => a.Code)
                  .IsRequired()
                  .HasMaxLength(10);

            entity.Property(a => a.Name)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(a => a.City)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.Country)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(a => a.Timezone)
                  .HasMaxLength(50);

            entity.HasIndex(a => a.Code).IsUnique();
        }
    }
}

