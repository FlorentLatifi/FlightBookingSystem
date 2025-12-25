using System;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Domain.Events
{
    /// <summary>
    /// Domain Event që shkaktohet kur pagesa kompletohet me sukses
    /// DESIGN PATTERN: Domain Events - Përdoret për të koordinuar veprime pas pagesës së suksesshme
    /// </summary>
    public class PaymentCompletedEvent
    {
        public Payment Payment { get; }
        public Reservation Reservation { get; }
        public DateTime OccurredAt { get; }

        public PaymentCompletedEvent(Payment payment, Reservation reservation)
        {
            Payment = payment ?? throw new ArgumentNullException(nameof(payment));
            Reservation = reservation ?? throw new ArgumentNullException(nameof(reservation));
            OccurredAt = DateTime.UtcNow;
        }
    }
}
