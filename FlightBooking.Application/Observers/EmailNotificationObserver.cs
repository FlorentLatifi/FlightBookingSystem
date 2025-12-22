using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using System.Text;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// Observer që dërgon njoftime përmes email
    /// </summary>
    public class EmailNotificationObserver : INotificationObserver
    {
        private readonly IEmailService _emailService;

        public string ObserverName => "Email Notification Observer";

        public EmailNotificationObserver(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Dërgon email konfirmimi për rezervimin
        /// </summary>
        public async Task OnReservationConfirmedAsync(Reservation reservation)
        {
            var subject = $"Konfirmimi i Rezervimit - {reservation.ReservationCode}";

            var body = BuildReservationConfirmationEmail(reservation);

            await _emailService.SendEmailAsync(
                toEmail: reservation.Passenger.Email,
                subject: subject,
                body: body,
                isHtml: true
            );

            Console.WriteLine($"[{ObserverName}] Email konfirmimi u dërgua te: {reservation.Passenger.Email}");
        }

        /// <summary>
        /// Dërgon email anulimi
        /// </summary>
        public async Task OnReservationCancelledAsync(Reservation reservation)
        {
            var subject = $"Anulimi i Rezervimit - {reservation.ReservationCode}";

            var body = BuildCancellationEmail(reservation);

            await _emailService.SendEmailAsync(
                toEmail: reservation.Passenger.Email,
                subject: subject,
                body: body,
                isHtml: true
            );

            Console.WriteLine($"[{ObserverName}] Email anulimi u dërgua te: {reservation.Passenger.Email}");
        }

        /// <summary>
        /// Dërgon email konfirmimi për pagesën
        /// </summary>
        public async Task OnPaymentCompletedAsync(Payment payment)
        {
            var subject = $"Konfirmimi i Pagesës - {payment.TransactionId}";

            var body = BuildPaymentConfirmationEmail(payment);

            await _emailService.SendEmailAsync(
                toEmail: payment.Reservation.Passenger.Email,
                subject: subject,
                body: body,
                isHtml: true
            );

            Console.WriteLine($"[{ObserverName}] Email konfirmimi i pagesës u dërgua te: {payment.Reservation.Passenger.Email}");
        }

        /// <summary>
        /// Ndërton HTML email për konfirmimin e rezervimit
        /// </summary>
        private string BuildReservationConfirmationEmail(Reservation reservation)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html><body style='font-family: Arial, sans-serif;'>");
            sb.AppendLine("<h2 style='color: #2c3e50;'>Rezervimi juaj u konfirmua!</h2>");
            sb.AppendLine($"<p>Përshëndetje <strong>{reservation.Passenger.FullName}</strong>,</p>");
            sb.AppendLine("<p>Rezervimi juaj u konfirmua me sukses. Ja detajet:</p>");
            sb.AppendLine("<div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px;'>");
            sb.AppendLine($"<p><strong>Kodi i Rezervimit:</strong> {reservation.ReservationCode}</p>");
            sb.AppendLine($"<p><strong>Fluturimi:</strong> {reservation.Flight.FlightNumber}</p>");
            sb.AppendLine($"<p><strong>Nga:</strong> {reservation.Flight.DepartureAirport}</p>");
            sb.AppendLine($"<p><strong>Për në:</strong> {reservation.Flight.ArrivalAirport}</p>");
            sb.AppendLine($"<p><strong>Nisja:</strong> {reservation.Flight.DepartureTime:dd/MM/yyyy HH:mm}</p>");
            sb.AppendLine($"<p><strong>Mbërritja:</strong> {reservation.Flight.ArrivalTime:dd/MM/yyyy HH:mm}</p>");
            sb.AppendLine($"<p><strong>Klasa:</strong> {reservation.SeatClass}</p>");
            sb.AppendLine($"<p><strong>Ulësja:</strong> {reservation.SeatNumber}</p>");
            sb.AppendLine($"<p><strong>Çmimi Total:</strong> €{reservation.TotalPrice:F2}</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("<p>Udhëtim të këndshëm!</p>");
            sb.AppendLine("<p style='color: #7f8c8d; font-size: 12px;'>Flight Booking System</p>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        /// <summary>
        /// Ndërton HTML email për anulimin e rezervimit
        /// </summary>
        private string BuildCancellationEmail(Reservation reservation)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html><body style='font-family: Arial, sans-serif;'>");
            sb.AppendLine("<h2 style='color: #e74c3c;'>Rezervimi juaj u anulua</h2>");
            sb.AppendLine($"<p>Përshëndetje <strong>{reservation.Passenger.FullName}</strong>,</p>");
            sb.AppendLine("<p>Rezervimi juaj u anulua me sukses.</p>");
            sb.AppendLine("<div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px;'>");
            sb.AppendLine($"<p><strong>Kodi i Rezervimit:</strong> {reservation.ReservationCode}</p>");
            sb.AppendLine($"<p><strong>Fluturimi:</strong> {reservation.Flight.FlightNumber}</p>");
            sb.AppendLine($"<p><strong>Çmimi i Rimbursuar:</strong> €{reservation.TotalPrice:F2}</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("<p>Rimbursi do të procesohet brenda 5-7 ditëve pune.</p>");
            sb.AppendLine("<p style='color: #7f8c8d; font-size: 12px;'>Flight Booking System</p>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }

        /// <summary>
        /// Ndërton HTML email për konfirmimin e pagesës
        /// </summary>
        private string BuildPaymentConfirmationEmail(Payment payment)
        {
            var sb = new StringBuilder();
            sb.AppendLine("<html><body style='font-family: Arial, sans-serif;'>");
            sb.AppendLine("<h2 style='color: #27ae60;'>Pagesa u krye me sukses!</h2>");
            sb.AppendLine($"<p>Përshëndetje <strong>{payment.Reservation.Passenger.FullName}</strong>,</p>");
            sb.AppendLine("<p>Pagesa juaj u procesua me sukses.</p>");
            sb.AppendLine("<div style='background-color: #f8f9fa; padding: 20px; border-radius: 5px;'>");
            sb.AppendLine($"<p><strong>Transaction ID:</strong> {payment.TransactionId}</p>");
            sb.AppendLine($"<p><strong>Shuma e Paguar:</strong> €{payment.Amount:F2}</p>");
            sb.AppendLine($"<p><strong>Metoda e Pagesës:</strong> {payment.PaymentMethod}</p>");
            sb.AppendLine($"<p><strong>Data:</strong> {payment.PaymentDate:dd/MM/yyyy HH:mm}</p>");
            sb.AppendLine("</div>");
            sb.AppendLine("<p>Faleminderit për rezervimin tuaj!</p>");
            sb.AppendLine("<p style='color: #7f8c8d; font-size: 12px;'>Flight Booking System</p>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}
