using System;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Domain.Events
{
    /// <summary>
    /// Domain Event që shkaktohet kur krijohet një rezervim i ri
    /// DESIGN PATTERN: Domain Events - Përdoret për të koordinuar veprime të ndryshme pas krijimit të rezervimit
    /// </summary>
    public class ReservationCreatedEvent
    {
        public Reservation Reservation { get; }
        public DateTime OccurredAt { get; }

        public ReservationCreatedEvent(Reservation reservation)
        {
            Reservation = reservation ?? throw new ArgumentNullException(nameof(reservation));
            OccurredAt = DateTime.UtcNow;
        }
    }
}
