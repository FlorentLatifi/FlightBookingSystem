using System;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using FlightBooking.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Application.Observers
{
    public class EmailNotificationObserver : IBookingObserver
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<EmailNotificationObserver> _logger;

        public string ObserverName => "Email Notification";

        public EmailNotificationObserver(
            IEmailService emailService,
            ILogger<EmailNotificationObserver> logger)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task NotifyAsync(BookingNotification notification)
        {
            try
            {
                _logger.LogInformation($"[{ObserverName}] Processing notification: {notification.Type}");

                string subject = notification.Type switch
                {
                    NotificationType.BookingCreated => "Booking Created - Action Required",
                    NotificationType.BookingConfirmed => "Booking Confirmed",
                    NotificationType.BookingCancelled => "Booking Cancelled",
                    NotificationType.PaymentReceived => "Payment Received",
                    _ => "Booking Update"
                };

                string body = GenerateEmailBody(notification);

                await _emailService.SendEmailAsync(
                    notification.Booking.PassengerEmail,
                    subject,
                    body);

                _logger.LogInformation($"[{ObserverName}] Email sent successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"[{ObserverName}] Failed to send email");
            }
        }

        private string GenerateEmailBody(BookingNotification notification)
        {
            var booking = notification.Booking;
            var flight = booking.Flight;

            return $@"
Dear {booking.PassengerName},

{notification.Message}

Booking Details:
================
Booking Reference: {booking.BookingReference}
Flight: {flight.FlightNumber}
Route: {flight.Origin} → {flight.Destination}
Departure: {flight.DepartureTime:dd MMM yyyy HH:mm}
Total Price: {booking.TotalPrice}
Status: {booking.Status}

Thank you!
";
        }
    }
}
