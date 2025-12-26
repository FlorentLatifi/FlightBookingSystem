using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> entity)
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Amount)
                  .HasColumnType("decimal(18,2)");

            entity.Property(p => p.Currency)
                  .IsRequired()
                  .HasMaxLength(3);

            entity.Property(p => p.PaymentMethod)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(p => p.TransactionId)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(p => p.PaymentGatewayResponse)
                  .HasMaxLength(500);

            entity.HasIndex(p => p.TransactionId).IsUnique();
        }
    }
}

