using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një fluturim
    /// </summary>
    public class Flight
    {
        /// <summary>
        /// ID unik i fluturimit
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Numri i fluturimit (p.sh. "AA123", "LH456")
        /// </summary>
        public string FlightNumber { get; set; } = string.Empty;

        /// <summary>
        /// Linja ajrore (airline) që operon fluturimin
        /// </summary>
        public string Airline { get; set; } = string.Empty;

        /// <summary>
        /// Aeroporti i nisjes (p.sh. "PRN - Prishtina", "TIA - Tirana")
        /// </summary>
        public string DepartureAirport { get; set; } = string.Empty;

        /// <summary>
        /// Aeroporti i destinacionit (p.sh. "FRA - Frankfurt", "MUC - Munich")
        /// </summary>
        public string ArrivalAirport { get; set; } = string.Empty;

        /// <summary>
        /// Data dhe koha e nisjes
        /// </summary>
        public DateTime DepartureTime { get; set; }

        /// <summary>
        /// Data dhe koha e mbërritjes
        /// </summary>
        public DateTime ArrivalTime { get; set; }

        /// <summary>
        /// Çmimi bazë i fluturimit (në Euro)
        /// </summary>
        public decimal BasePrice { get; set; }

        /// <summary>
        /// Numri total i ulëseve në fluturim
        /// </summary>
        public int TotalSeats { get; set; }

        /// <summary>
        /// Numri i ulëseve të disponueshme (që nuk janë rezervuar)
        /// </summary>
        public int AvailableSeats { get; set; }

        /// <summary>
        /// Statusi aktual i fluturimit
        /// </summary>
        public FlightStatus Status { get; set; }

        /// <summary>
        /// Kohëzgjatja e fluturimit në minuta
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Lista e rezervimeve për këtë fluturim
        /// Navigation property për Entity Framework
        /// </summary>
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        /// <summary>
        /// A ka vende të disponueshme?
        /// Business logic method
        /// </summary>
        public bool HasAvailableSeats()
        {
            return AvailableSeats > 0;
        }

        /// <summary>
        /// A mund të rezervohet ky fluturim?
        /// </summary>
        public bool CanBeBooked()
        {
            return Status == FlightStatus.Scheduled
                   && HasAvailableSeats()
                   && DepartureTime > DateTime.Now;
        }
    }
}