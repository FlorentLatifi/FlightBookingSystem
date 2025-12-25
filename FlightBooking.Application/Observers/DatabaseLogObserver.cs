using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// Observer për logimin e njoftimeve në database
    /// Për momentin është disabled sepse BookingLog entity dhe IBookingLogRepository mungojnë
    /// </summary>
    public class DatabaseLogObserver : IBookingObserver
    {
        private readonly ILogger<DatabaseLogObserver> _logger;

        public string ObserverName => "Database Logger";

        public DatabaseLogObserver(ILogger<DatabaseLogObserver> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task NotifyAsync(BookingNotification notification)
        {
            try
            {
                _logger.LogInformation("[{ObserverName}] Logging notification: {NotificationType} for booking {BookingId}",
                    ObserverName, notification.Type, notification.Booking.Id);

                // TODO: Implement database logging when BookingLog entity and repository are available
                // For now, just log to console/file
                await Task.CompletedTask;

                _logger.LogInformation("[{ObserverName}] Log saved successfully", ObserverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to save log", ObserverName);
            }
        }
    }
}
