using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// DESIGN PATTERN: Observer Pattern
    /// Interface për dërgimin e njoftimeve
    /// Koordinon observers që reagojnë ndaj ngjarjeve të rezervimeve
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// DESIGN PATTERN: Parallel Processing
        /// Përgatit njoftimet për rezervimin (përdoret për procesim paralel)
        /// </summary>
        Task PrepareNotificationsAsync(Reservation reservation);

        /// <summary>
        /// DESIGN PATTERN: Parallel Processing
        /// Dërgon njoftimet e përgatitura (pas pagesës së suksesshme)
        /// </summary>
        Task SendPreparedNotificationsAsync();

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern
        /// Dërgon njoftim konfirmimi për rezervimin
        /// </summary>
        Task SendReservationConfirmationAsync(Reservation reservation);

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern
        /// Dërgon njoftim anulimi
        /// </summary>
        Task SendCancellationNotificationAsync(Reservation reservation);

        /// <summary>
        /// DESIGN PATTERN: Observer Pattern
        /// Dërgon njoftim për pagesën e suksesshme
        /// </summary>
        Task SendPaymentConfirmationAsync(Payment payment);
    }
}
