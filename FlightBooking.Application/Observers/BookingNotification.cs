using System;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Observers
{
    public class BookingNotification
    {
        public Booking Booking { get; set; }
        public NotificationType Type { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; }

        public BookingNotification(Booking booking, NotificationType type, string message)
        {
            Booking = booking ?? throw new ArgumentNullException(nameof(booking));
            Type = type;
            Message = message ?? string.Empty;
            Timestamp = DateTime.UtcNow;
        }
    }

    public enum NotificationType
    {
        BookingCreated,
        BookingConfirmed,
        BookingCancelled,
        PaymentReceived
    }
}