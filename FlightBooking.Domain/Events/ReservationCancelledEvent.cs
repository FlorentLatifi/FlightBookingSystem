using System;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Domain.Events
{
    /// <summary>
    /// Domain Event që shkaktohet kur anulohet një rezervim
    /// DESIGN PATTERN: Domain Events - Përdoret për të koordinuar veprime pas anulimit të rezervimit
    /// </summary>
    public class ReservationCancelledEvent
    {
        public Reservation Reservation { get; }
        public string CancellationReason { get; }
        public DateTime OccurredAt { get; }

        public ReservationCancelledEvent(Reservation reservation, string cancellationReason = "")
        {
            Reservation = reservation ?? throw new ArgumentNullException(nameof(reservation));
            CancellationReason = cancellationReason;
            OccurredAt = DateTime.UtcNow;
        }
    }
}
