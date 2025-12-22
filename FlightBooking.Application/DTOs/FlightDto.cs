using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për të shfaqur detajet e një fluturimi
    /// Përdoret për të transferuar të dhëna nga server te klienti
    /// </summary>
    public class FlightDto
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }
        public int AvailableSeats { get; set; }
        public FlightStatus Status { get; set; }
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Kohëzgjatja në format të lexueshëm (p.sh. "2h 30m")
        /// </summary>
        public string FormattedDuration
        {
            get
            {
                var hours = DurationMinutes / 60;
                var minutes = DurationMinutes % 60;
                return $"{hours}h {minutes}m";
            }
        }

        /// <summary>
        /// A ka vende të disponueshme?
        /// </summary>
        public bool IsAvailable => AvailableSeats > 0 && Status == FlightStatus.Scheduled;
    }
}
