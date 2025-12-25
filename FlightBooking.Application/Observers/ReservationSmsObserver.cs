using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// DESIGN PATTERN: Observer Pattern
    /// Observer që dërgon SMS notifications për rezervime
    /// Implementon INotificationObserver për të reaguar ndaj ngjarjeve të rezervimeve
    /// </summary>
    public class ReservationSmsObserver : INotificationObserver
    {
        private readonly ISmsService _smsService;
        private readonly ILogger<ReservationSmsObserver> _logger;

        public string ObserverName => "SMS Notification";

        public ReservationSmsObserver(
            ISmsService smsService,
            ILogger<ReservationSmsObserver> logger)
        {
            _smsService = smsService ?? throw new ArgumentNullException(nameof(smsService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task OnReservationConfirmedAsync(Reservation reservation)
        {
            try
            {
                _logger.LogInformation("[{ObserverName}] Sending confirmation SMS for reservation {ReservationCode}",
                    ObserverName, reservation.ReservationCode);

                var message = $"Rezervimi {reservation.ReservationCode} u konfirmua! Fluturimi {reservation.Flight.FlightNumber} më {reservation.Flight.DepartureTime:dd MMM}.";

                await _smsService.SendSmsAsync(
                    reservation.Passenger.PhoneNumber,
                    message);

                _logger.LogInformation("[{ObserverName}] Confirmation SMS sent successfully", ObserverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to send confirmation SMS", ObserverName);
            }
        }

        public async Task OnReservationCancelledAsync(Reservation reservation)
        {
            try
            {
                _logger.LogInformation("[{ObserverName}] Sending cancellation SMS for reservation {ReservationCode}",
                    ObserverName, reservation.ReservationCode);

                var message = $"Rezervimi {reservation.ReservationCode} u anulua. Rimbursi në 5-7 ditë.";

                await _smsService.SendSmsAsync(
                    reservation.Passenger.PhoneNumber,
                    message);

                _logger.LogInformation("[{ObserverName}] Cancellation SMS sent successfully", ObserverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to send cancellation SMS", ObserverName);
            }
        }

        public async Task OnPaymentCompletedAsync(Payment payment)
        {
            // SMS për pagesë nuk dërgohet - vetëm për konfirmim dhe anulim
            await Task.CompletedTask;
            _logger.LogInformation("[{ObserverName}] Skipped SMS for payment completion", ObserverName);
        }
    }
}

