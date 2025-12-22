using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Observers
{
    /// <summary>
    /// Interface për observers që duan të marrin njoftime
    /// OBSERVER PATTERN - Lejon multiple objects të reagojnë ndaj ngjarjeve
    /// </summary>
    public interface INotificationObserver
    {
        /// <summary>
        /// Thirret kur një rezervim konfirmohet
        /// </summary>
        Task OnReservationConfirmedAsync(Reservation reservation);

        /// <summary>
        /// Thirret kur një rezervim anulohet
        /// </summary>
        Task OnReservationCancelledAsync(Reservation reservation);

        /// <summary>
        /// Thirret kur pagesa kompletohet me sukses
        /// </summary>
        Task OnPaymentCompletedAsync(Payment payment);

        /// <summary>
        /// Emri i observer-it (për logging)
        /// </summary>
        string ObserverName { get; }
    }
}
