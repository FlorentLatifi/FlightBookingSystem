using System;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Payment entity - supports both Reservation and Booking systems
    /// </summary>
    public class Payment
    {
        public int Id { get; set; }

        // For Reservation system
        public int ReservationId { get; set; }
        public virtual Reservation? Reservation { get; set; }

        // For Booking system (NEW - supports dual architecture)
        public int? BookingId { get; set; }
        public virtual Booking? Booking { get; set; }

        public decimal Amount { get; set; }
        public string Currency { get; set; } = "USD";
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public string? PaymentGatewayResponse { get; set; }

        /// <summary>
        /// Is payment successful (for backward compatibility)
        /// </summary>
        public bool IsSuccessful => Status == PaymentStatus.Completed;

        public Payment()
        {
            PaymentDate = DateTime.UtcNow;
            Status = PaymentStatus.Pending;
        }

        public bool CanBeRefunded()
        {
            return Status == PaymentStatus.Completed &&
                   ProcessedDate.HasValue &&
                   ProcessedDate.Value.AddDays(30) > DateTime.UtcNow;
        }
    }
}