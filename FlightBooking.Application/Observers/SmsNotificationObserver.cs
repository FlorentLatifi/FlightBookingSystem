using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using FlightBooking.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Application.Observers
{
    public class SmsNotificationObserver : IBookingObserver
    {
        private readonly ISmsService _smsService;
        private readonly ILogger<SmsNotificationObserver> _logger;

        public string ObserverName => "SMS Notification";

        public SmsNotificationObserver(
            ISmsService smsService,
            ILogger<SmsNotificationObserver> logger)
        {
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task NotifyAsync(BookingNotification notification)
        {
            try
            {
                _logger.LogInformation("[{ObserverName}] Processing notification: {NotificationType}",
                    ObserverName, notification.Type);

                // Only send SMS for critical updates
                if (notification.Type == NotificationType.BookingConfirmed ||
                    notification.Type == NotificationType.BookingCancelled)
                {
                    string message = GenerateSmsMessage(notification);

                    await _smsService.SendSmsAsync(
                        notification.Booking.PassengerPhone,
                        message);

                    _logger.LogInformation("[{ObserverName}] SMS sent successfully", ObserverName);
                }
                else
                {
                    _logger.LogInformation("[{ObserverName}] Skipped SMS for notification type: {NotificationType}",
                        ObserverName, notification.Type);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to send SMS", ObserverName);
            }
        }

        private static string GenerateSmsMessage(BookingNotification notification)
        {
            var booking = notification.Booking;

            return notification.Type switch
            {
                NotificationType.BookingConfirmed =>
                    $"Booking {booking.BookingReference} confirmed! Flight {booking.Flight.FlightNumber} on {booking.Flight.DepartureTime:dd MMM}.",

                NotificationType.BookingCancelled =>
                    $"Booking {booking.BookingReference} cancelled. Refund in 5-7 days.",

                _ => notification.Message
            };
        }
    }
}