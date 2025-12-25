using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Observers;
using FlightBooking.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Application.Services
{
    /// <summary>
    /// DESIGN PATTERN: Observer Pattern
    /// Service për menaxhimin e njoftimeve
    /// Koordinon observers që reagojnë ndaj ngjarjeve të rezervimeve
    /// </summary>
    public class NotificationService : INotificationService
    {
        private readonly NotificationSubject _notificationSubject;
        private readonly ILogger<NotificationService>? _logger;
        private Reservation? _preparedReservation;
        private Payment? _preparedPayment;

        public NotificationService(
            IEmailService emailService,
            IEnumerable<INotificationObserver> observers,
            ILogger<NotificationService>? logger = null)
        {
            _notificationSubject = new NotificationSubject();
            _logger = logger;

            // Regjistro të gjithë observers
            foreach (var observer in observers)
            {
                _notificationSubject.Attach(observer);
            }

            Console.WriteLine($"[NotificationService] U inicializua me {_notificationSubject.GetObserverCount()} observers");
            _logger?.LogInformation("[NotificationService] U inicializua me {ObserverCount} observers", _notificationSubject.GetObserverCount());
        }

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern - Procesim Paralel
        /// Përgatit njoftimet për rezervimin (mund të ekzekutohet paralel me pagesën)
        /// </summary>
        public async Task PrepareNotificationsAsync(Reservation reservation)
        {
            _logger?.LogInformation("[NotificationService] 🔥 [PARALLEL] Përgatitja e njoftimeve për rezervimin {ReservationCode}",
                reservation.ReservationCode);

            // Ruaj rezervimin për dërgim më vonë
            _preparedReservation = reservation;

            // Simulon përgatitjen e njoftimeve (templates, data gathering, etj.)
            await Task.Delay(300); // Simulon përgatitjen

            _logger?.LogInformation("[NotificationService] ✅ [PARALLEL] Njoftimet u përgatitën për rezervimin {ReservationCode}",
                reservation.ReservationCode);
        }

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern - Procesim Paralel
        /// Dërgon njoftimet e përgatitura (pas pagesës së suksesshme)
        /// </summary>
        public async Task SendPreparedNotificationsAsync()
        {
            if (_preparedReservation == null)
            {
                _logger?.LogWarning("[NotificationService] Nuk ka njoftime të përgatitura për dërgim");
                return;
            }

            _logger?.LogInformation("[NotificationService] 🔥 [PARALLEL] Duke dërguar njoftimet e përgatitura...");

            // Dërgo njoftimet për rezervimin
            await SendReservationConfirmationAsync(_preparedReservation);

            // Nëse ka pagesë të përgatitur, dërgo edhe atë
            if (_preparedPayment != null)
            {
                await SendPaymentConfirmationAsync(_preparedPayment);
            }

            // Pastro përgatitjet
            _preparedReservation = null;
            _preparedPayment = null;
        }

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern
        /// Dërgon njoftim konfirmimi për rezervimin
        /// Të gjithë observers (Email, SMS) ekzekutohen NË PARALEL
        /// </summary>
        public async Task SendReservationConfirmationAsync(Reservation reservation)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[NotificationService] 🔥 [OBSERVER PATTERN] Duke dërguar njoftim konfirmimi...");
            Console.WriteLine("============================================");
            _logger?.LogInformation("[NotificationService] 🔥 [OBSERVER PATTERN] Duke dërguar njoftim konfirmimi për rezervimin {ReservationCode}",
                reservation.ReservationCode);

            // DESIGN PATTERN: Observer Pattern - Paralel Execution
            // Të gjithë observers ekzekutohen në të njëjtën kohë
            await _notificationSubject.NotifyReservationConfirmedAsync(reservation);

            Console.WriteLine("============================================\n");
            _logger?.LogInformation("[NotificationService] ✅ Njoftimi i konfirmimit u dërgua me sukses");
        }

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern
        /// Dërgon njoftim anulimi
        /// Të gjithë observers (Email, SMS) ekzekutohen NË PARALEL
        /// </summary>
        public async Task SendCancellationNotificationAsync(Reservation reservation)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[NotificationService] 🔥 [OBSERVER PATTERN] Duke dërguar njoftim anulimi...");
            Console.WriteLine("============================================");
            _logger?.LogInformation("[NotificationService] 🔥 [OBSERVER PATTERN] Duke dërguar njoftim anulimi për rezervimin {ReservationCode}",
                reservation.ReservationCode);

            // DESIGN PATTERN: Observer Pattern - Paralel Execution
            await _notificationSubject.NotifyReservationCancelledAsync(reservation);

            Console.WriteLine("============================================\n");
            _logger?.LogInformation("[NotificationService] ✅ Njoftimi i anulimit u dërgua me sukses");
        }

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern
        /// Dërgon njoftim për pagesën e suksesshme
        /// Të gjithë observers (Email, SMS) ekzekutohen NË PARALEL
        /// </summary>
        public async Task SendPaymentConfirmationAsync(Payment payment)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[NotificationService] 🔥 [OBSERVER PATTERN] Duke dërguar njoftim pagese...");
            Console.WriteLine("============================================");
            _logger?.LogInformation("[NotificationService] 🔥 [OBSERVER PATTERN] Duke dërguar njoftim pagese për Transaction ID {TransactionId}",
                payment.TransactionId);

            // Ruaj pagesën për përdorim në PrepareNotificationsAsync
            _preparedPayment = payment;

            // DESIGN PATTERN: Observer Pattern - Paralel Execution
            await _notificationSubject.NotifyPaymentCompletedAsync(payment);

            Console.WriteLine("============================================\n");
            _logger?.LogInformation("[NotificationService] ✅ Njoftimi i pagesës u dërgua me sukses");
        }
    }
}
