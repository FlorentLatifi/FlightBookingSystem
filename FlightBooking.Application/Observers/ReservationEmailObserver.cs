using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// DESIGN PATTERN: Observer Pattern
    /// Observer që dërgon email notifications për rezervime
    /// Implementon INotificationObserver për të reaguar ndaj ngjarjeve të rezervimeve
    /// </summary>
    public class ReservationEmailObserver : INotificationObserver
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<ReservationEmailObserver> _logger;

        public string ObserverName => "Email Notification";

        public ReservationEmailObserver(
            IEmailService emailService,
            ILogger<ReservationEmailObserver> logger)
        {
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task OnReservationConfirmedAsync(Reservation reservation)
        {
            try
            {
                _logger.LogInformation("[{ObserverName}] Sending confirmation email for reservation {ReservationCode}",
                    ObserverName, reservation.ReservationCode);

                var subject = "Rezervimi Juaj është Konfirmuar";
                var body = GenerateConfirmationEmailBody(reservation);

                await _emailService.SendEmailAsync(
                    reservation.Passenger.Email,
                    subject,
                    body);

                _logger.LogInformation("[{ObserverName}] Confirmation email sent successfully", ObserverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to send confirmation email", ObserverName);
            }
        }

        public async Task OnReservationCancelledAsync(Reservation reservation)
        {
            try
            {
                _logger.LogInformation("[{ObserverName}] Sending cancellation email for reservation {ReservationCode}",
                    ObserverName, reservation.ReservationCode);

                var subject = "Rezervimi Juaj është Anuluar";
                var body = GenerateCancellationEmailBody(reservation);

                await _emailService.SendEmailAsync(
                    reservation.Passenger.Email,
                    subject,
                    body);

                _logger.LogInformation("[{ObserverName}] Cancellation email sent successfully", ObserverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to send cancellation email", ObserverName);
            }
        }

        public async Task OnPaymentCompletedAsync(Payment payment)
        {
            try
            {
                if (payment.Reservation == null)
                {
                    _logger.LogWarning("[{ObserverName}] Payment {PaymentId} has no reservation", ObserverName, payment.Id);
                    return;
                }

                _logger.LogInformation("[{ObserverName}] Sending payment confirmation email for reservation {ReservationCode}",
                    ObserverName, payment.Reservation.ReservationCode);

                var subject = "Pagesa Juaj është Pranuar";
                var body = GeneratePaymentEmailBody(payment);

                await _emailService.SendEmailAsync(
                    payment.Reservation.Passenger.Email,
                    subject,
                    body);

                _logger.LogInformation("[{ObserverName}] Payment confirmation email sent successfully", ObserverName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "[{ObserverName}] Failed to send payment confirmation email", ObserverName);
            }
        }

        private string GenerateConfirmationEmailBody(Reservation reservation)
        {
            var flight = reservation.Flight;
            return $@"
Dear {reservation.Passenger.FirstName} {reservation.Passenger.LastName},

Rezervimi juaj është konfirmuar me sukses!

Detajet e Rezervimit:
====================
Kodi i Rezervimit: {reservation.ReservationCode}
Fluturimi: {flight.FlightNumber}
Kompania: {flight.Airline}
Rruga: {flight.DepartureAirport} → {flight.ArrivalAirport}
Nisja: {flight.DepartureTime:dd MMM yyyy HH:mm}
Arritja: {flight.ArrivalTime:dd MMM yyyy HH:mm}
Klasa: {reservation.SeatClass}
Ulësja: {reservation.SeatNumber}
Çmimi Total: €{reservation.TotalPrice:F2}

Ju falënderojmë për rezervimin tuaj!
Have a great flight!

Flight Booking System
";
        }

        private string GenerateCancellationEmailBody(Reservation reservation)
        {
            return $@"
Dear {reservation.Passenger.FirstName} {reservation.Passenger.LastName},

Rezervimi juaj me kod {reservation.ReservationCode} është anuluar.

Rimbursimi do të procesohet brenda 5-7 ditëve në kartën tuaj.

Nëse keni pyetje, ju lutem na kontaktoni.

Flight Booking System
";
        }

        private string GeneratePaymentEmailBody(Payment payment)
        {
            if (payment.Reservation == null)
                return "Payment received.";

            var reservation = payment.Reservation;
            return $@"
Dear {reservation.Passenger.FirstName} {reservation.Passenger.LastName},

Pagesa juaj prej €{payment.Amount:F2} është pranuar me sukses!

Transaction ID: {payment.TransactionId}
Data e Pagesës: {payment.PaymentDate:dd MMM yyyy HH:mm}

Rezervimi juaj është tani i konfirmuar.

Flight Booking System
";
        }
    }
}

