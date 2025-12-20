using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një pasagjer
    /// </summary>
    public class Passenger
    {
        /// <summary>
        /// ID unik i pasagjerit
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Emri i pasagjerit
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Mbiemri i pasagjerit
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Email adresa e pasagjerit
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Numri i telefonit
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;

        /// <summary>
        /// Numri i pasaportës
        /// </summary>
        public string PassportNumber { get; set; } = string.Empty;

        /// <summary>
        /// Data e lindjes
        /// </summary>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Kombësia
        /// </summary>
        public string Nationality { get; set; } = string.Empty;

        /// <summary>
        /// Lista e rezervimeve të këtij pasagjeri
        /// Navigation property
        /// </summary>
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        /// <summary>
        /// Emri i plotë i pasagjerit
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        /// <summary>
        /// A është pasagjeri adult? (mbi 18 vjeç)
        /// </summary>
        public bool IsAdult()
        {
            var age = DateTime.Now.Year - DateOfBirth.Year;
            if (DateTime.Now.DayOfYear < DateOfBirth.DayOfYear)
                age--;
            return age >= 18;
        }
    }
}
