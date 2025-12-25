using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Entities
{
    public class Airline
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // W6, LH, OS
        public string Name { get; set; } = string.Empty; // Wizz Air, Lufthansa
        public string Country { get; set; } = string.Empty;
        public string LogoUrl { get; set; } = string.Empty;

        // Navigation
        public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
    }
}