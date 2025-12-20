using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një rezervim
    /// Lidhja midis një pasagjeri dhe një fluturimi
    /// </summary>
    public class Reservation
    {
        /// <summary>
        /// ID unik i rezervimit
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Kodi i rezervimit (p.sh. "RES-ABC123")
        /// Ky kod i jepet klientit për identifikim
        /// </summary>
        public string ReservationCode { get; set; } = string.Empty;

        /// <summary>
        /// ID e fluturimit që rezervohet
        /// Foreign Key
        /// </summary>
        public int FlightId { get; set; }

        /// <summary>
        /// Fluturimi që rezervohet
        /// Navigation property
        /// </summary>
        public virtual Flight Flight { get; set; } = null!;

        /// <summary>
        /// ID e pasagjerit që bën rezervimin
        /// Foreign Key
        /// </summary>
        public int PassengerId { get; set; }

        /// <summary>
        /// Pasagjeri që bën rezervimin
        /// Navigation property
        /// </summary>
        public virtual Passenger Passenger { get; set; } = null!;

        /// <summary>
        /// Klasa e ulëses së zgjedhur
        /// </summary>
        public SeatClass SeatClass { get; set; }

        /// <summary>
        /// Numri i ulëses (p.sh. "12A", "5C")
        /// </summary>
        public string SeatNumber { get; set; } = string.Empty;

        /// <summary>
        /// Çmimi total i rezervimit (duke përfshirë çdo zbritje apo shtesë)
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Statusi i rezervimit
        /// </summary>
        public ReservationStatus Status { get; set; }

        /// <summary>
        /// Data dhe koha kur është bërë rezervimi
        /// </summary>
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Pagesa e lidhur me këtë rezervim
        /// Navigation property (1-to-1)
        /// </summary>
        public virtual Payment? Payment { get; set; }

        /// <summary>
        /// A është konfirmuar rezervimi?
        /// </summary>
        public bool IsConfirmed()
        {
            return Status == ReservationStatus.Confirmed;
        }

        /// <summary>
        /// A mund të anulohet rezervimi?
        /// </summary>
        public bool CanBeCancelled()
        {
            // Mund të anulohet vetëm nëse është Pending ose Confirmed
            // dhe fluturimi nuk ka nisur ende
            return (Status == ReservationStatus.Pending || Status == ReservationStatus.Confirmed)
                   && Flight.DepartureTime > DateTime.Now.AddHours(24); // 24 orë para nisjes
        }
    }
}
