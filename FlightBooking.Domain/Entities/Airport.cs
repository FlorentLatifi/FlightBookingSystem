using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Entities
{
    public class Airport
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty; // PRN, VIE, MUC
        public string Name { get; set; } = string.Empty; // Prishtina International Airport
        public string City { get; set; } = string.Empty; // Prishtina
        public string Country { get; set; } = string.Empty; // Kosovo
        public string Timezone { get; set; } = string.Empty; // Europe/Belgrade

        // Navigation
        public virtual ICollection<Flight> DepartureFlights { get; set; } = new List<Flight>();
        public virtual ICollection<Flight> ArrivalFlights { get; set; } = new List<Flight>();
    }
}
