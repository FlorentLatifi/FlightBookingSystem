using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Enums
{
    /// <summary>
    /// Statuset e një rezervimi
    /// </summary>
    public enum ReservationStatus
    {
        /// <summary>
        /// Rezervimi është në pritje të pagesës
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Rezervimi është konfirmuar (pagesa e kryer)
        /// </summary>
        Confirmed = 2,

        /// <summary>
        /// Rezervimi është anuluar
        /// </summary>
        Cancelled = 3,

        /// <summary>
        /// Check-in është bërë
        /// </summary>
        CheckedIn = 4,

        /// <summary>
        /// Fluturimi ka përfunduar
        /// </summary>
        Completed = 5
    }
}
