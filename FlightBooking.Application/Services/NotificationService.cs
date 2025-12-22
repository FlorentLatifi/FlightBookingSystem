using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Observers;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Services
{
    /// <summary>
    /// Service për menaxhimin e njoftimeve
    /// PËRDOR OBSERVER PATTERN - koordinon observers
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly NotificationSubject _notificationSubject;

        public NotificationService(
            IEmailService emailService,
            IEnumerable<INotificationObserver> observers)
        {
            _notificationSubject = new NotificationSubject();

            // Regjistro të gjithë observers
            foreach (var observer in observers)
            {
                _notificationSubject.Attach(observer);
            }

            Console.WriteLine($"[NotificationService] U inicializua me {_notificationSubject.GetObserverCount()} observers");
        }

        /// <summary>
        /// Dërgon njoftim konfirmimi për rezervimin
        /// </summary>
        public async Task SendReservationConfirmationAsync(Reservation reservation)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[NotificationService] Duke dërguar njoftim konfirmimi...");
            Console.WriteLine("============================================");

            await _notificationSubject.NotifyReservationConfirmedAsync(reservation);

            Console.WriteLine("============================================\n");
        }

        /// <summary>
        /// Dërgon njoftim anulimi
        /// </summary>
        public async Task SendCancellationNotificationAsync(Reservation reservation)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[NotificationService] Duke dërguar njoftim anulimi...");
            Console.WriteLine("============================================");

            await _notificationSubject.NotifyReservationCancelledAsync(reservation);

            Console.WriteLine("============================================\n");
        }

        /// <summary>
        /// Dërgon njoftim për pagesën e suksesshme
        /// </summary>
        public async Task SendPaymentConfirmationAsync(Payment payment)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[NotificationService] Duke dërguar njoftim pagese...");
            Console.WriteLine("============================================");

            await _notificationSubject.NotifyPaymentCompletedAsync(payment);

            Console.WriteLine("============================================\n");
        }
    }
}
