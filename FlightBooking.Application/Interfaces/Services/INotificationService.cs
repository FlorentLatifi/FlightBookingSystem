using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// Interface për dërgimin e njoftimeve
    /// Ky service do të përdorë Observer Pattern
    /// </summary>
    public interface INotificationService
    {
        /// <summary>
        /// Dërgon njoftim konfirmimi për rezervimin
        /// </summary>
        Task SendReservationConfirmationAsync(Reservation reservation);

        /// <summary>
        /// Dërgon njoftim anulimi
        /// </summary>
        Task SendCancellationNotificationAsync(Reservation reservation);

        /// <summary>
        /// Dërgon njoftim për pagesën e suksesshme
        /// </summary>
        Task SendPaymentConfirmationAsync(Payment payment);
    }
}
