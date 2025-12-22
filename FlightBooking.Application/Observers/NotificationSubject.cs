using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// Subject që menaxhon observers dhe i njofton ata për ngjarje
    /// OBSERVER PATTERN - Ky është "subject" që observers e vëzhgojnë
    /// </summary>
    public class NotificationSubject
    {
        private readonly List<INotificationObserver> _observers = new();

        /// <summary>
        /// Shto një observer në listë
        /// </summary>
        public void Attach(INotificationObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                Console.WriteLine($"[NotificationSubject] Observer '{observer.ObserverName}' u shtua.");
            }
        }

        /// <summary>
        /// Hiq një observer nga lista
        /// </summary>
        public void Detach(INotificationObserver observer)
        {
            if (_observers.Contains(observer))
            {
                _observers.Remove(observer);
                Console.WriteLine($"[NotificationSubject] Observer '{observer.ObserverName}' u hoq.");
            }
        }

        /// <summary>
        /// Njofton të gjithë observers për konfirmimin e rezervimit
        /// </summary>
        public async Task NotifyReservationConfirmedAsync(Reservation reservation)
        {
            Console.WriteLine($"[NotificationSubject] Duke njoftuar {_observers.Count} observers për rezervimin e konfirmuar...");

            // Njofto të gjithë observers në PARALEL (simulon procesimin parallel nga provimi)
            var tasks = _observers.Select(observer =>
                observer.OnReservationConfirmedAsync(reservation)
            );

            await Task.WhenAll(tasks);

            Console.WriteLine($"[NotificationSubject] Të gjithë observers u njoftuan me sukses.");
        }

        /// <summary>
        /// Njofton të gjithë observers për anulimin e rezervimit
        /// </summary>
        public async Task NotifyReservationCancelledAsync(Reservation reservation)
        {
            Console.WriteLine($"[NotificationSubject] Duke njoftuar {_observers.Count} observers për rezervimin e anuluar...");

            var tasks = _observers.Select(observer =>
                observer.OnReservationCancelledAsync(reservation)
            );

            await Task.WhenAll(tasks);

            Console.WriteLine($"[NotificationSubject] Të gjithë observers u njoftuan me sukses.");
        }

        /// <summary>
        /// Njofton të gjithë observers për pagesën e kompletuar
        /// </summary>
        public async Task NotifyPaymentCompletedAsync(Payment payment)
        {
            Console.WriteLine($"[NotificationSubject] Duke njoftuar {_observers.Count} observers për pagesën e kompletuar...");

            var tasks = _observers.Select(observer =>
                observer.OnPaymentCompletedAsync(payment)
            );

            await Task.WhenAll(tasks);

            Console.WriteLine($"[NotificationSubject] Të gjithë observers u njoftuan me sukses.");
        }

        /// <summary>
        /// Merr numrin e observers të regjistruar
        /// </summary>
        public int GetObserverCount() => _observers.Count;
    }
}
