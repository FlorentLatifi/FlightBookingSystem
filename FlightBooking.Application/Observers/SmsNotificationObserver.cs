using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// Observer që dërgon njoftime përmes SMS
    /// SHËNIM: Ky është një mock implementation për demonstrim
    /// Në një aplikacion real, do të integrohet me një SMS gateway (Twilio, Nexmo, etj.)
    /// </summary>
    public class SmsNotificationObserver : INotificationObserver
    {
        public string ObserverName => "SMS Notification Observer";

        /// <summary>
        /// Dërgon SMS konfirmimi për rezervimin
        /// </summary>
        public async Task OnReservationConfirmedAsync(Reservation reservation)
        {
            var message = $"Rezervimi juaj {reservation.ReservationCode} u konfirmua! " +
                         $"Fluturimi: {reservation.Flight.FlightNumber} " +
                         $"Nisja: {reservation.Flight.DepartureTime:dd/MM/yyyy HH:mm}";

            await SendSmsAsync(reservation.Passenger.PhoneNumber, message);

            Console.WriteLine($"[{ObserverName}] SMS konfirmimi u dërgua te: {reservation.Passenger.PhoneNumber}");
        }

        /// <summary>
        /// Dërgon SMS anulimi
        /// </summary>
        public async Task OnReservationCancelledAsync(Reservation reservation)
        {
            var message = $"Rezervimi juaj {reservation.ReservationCode} u anulua. " +
                         $"Rimbursi: €{reservation.TotalPrice:F2}. " +
                         $"Do të procesohet brenda 5-7 ditëve.";

            await SendSmsAsync(reservation.Passenger.PhoneNumber, message);

            Console.WriteLine($"[{ObserverName}] SMS anulimi u dërgua te: {reservation.Passenger.PhoneNumber}");
        }

        /// <summary>
        /// Dërgon SMS konfirmimi për pagesën
        /// </summary>
        public async Task OnPaymentCompletedAsync(Payment payment)
        {
            var message = $"Pagesa juaj €{payment.Amount:F2} u krye me sukses. " +
                         $"Transaction ID: {payment.TransactionId}";

            await SendSmsAsync(payment.Reservation.Passenger.PhoneNumber, message);

            Console.WriteLine($"[{ObserverName}] SMS konfirmimi i pagesës u dërgua te: {payment.Reservation.Passenger.PhoneNumber}");
        }

        /// <summary>
        /// Simulon dërgimin e SMS
        /// Në një aplikacion real, këtu do të integrohej me një SMS gateway
        /// </summary>
        private async Task SendSmsAsync(string phoneNumber, string message)
        {
            // Simulojmë vonesën e dërgimit
            await Task.Delay(100);

            // Log në console për demonstrim
            Console.WriteLine($"[SMS API Mock] Dërgohet SMS te {phoneNumber}:");
            Console.WriteLine($"[SMS API Mock] Mesazhi: {message}");

            // Në një aplikacion real, këtu do të thërritej API e SMS provider:
            // await _smsClient.SendMessageAsync(phoneNumber, message);
        }
    }
}
