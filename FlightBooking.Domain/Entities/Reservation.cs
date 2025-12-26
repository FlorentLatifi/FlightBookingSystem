using System;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një rezervim
    /// Lidhja midis një pasagjeri dhe një fluturimi
    /// </summary>
    public class Reservation
    {
        public int Id { get; set; }
        public string ReservationCode { get; set; } = string.Empty;

        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; } = null!;

        public int PassengerId { get; set; }
        public virtual Passenger Passenger { get; set; } = null!;

        public SeatClass SeatClass { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Data kur u krijua rezervimi (për tracking)
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Pagesa e lidhur me këtë rezervim
        /// </summary>
        public virtual Payment? Payment { get; set; }

        /// <summary>
        /// A është konfirmuar rezervimi? (computed property)
        /// </summary>
        public bool IsConfirmed => Status == ReservationStatus.Confirmed;

        /// <summary>
        /// A mund të anulohet rezervimi?
        /// </summary>
        public bool CanBeCancelled()
        {
            return (Status == ReservationStatus.Pending || Status == ReservationStatus.Confirmed)
                   && Flight != null
                   && Flight.DepartureTime > DateTime.Now.AddHours(24);
        }
    }
}
