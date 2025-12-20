using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Enums
{
    /// <summary>
    /// Statuset e pagesës
    /// </summary>
    public enum PaymentStatus
    {
        /// <summary>
        /// Pagesa është në pritje
        /// </summary>
        Pending = 1,

        /// <summary>
        /// Pagesa po procesohet
        /// </summary>
        Processing = 2,

        /// <summary>
        /// Pagesa është kryer me sukses
        /// </summary>
        Completed = 3,

        /// <summary>
        /// Pagesa ka dështuar
        /// </summary>
        Failed = 4,

        /// <summary>
        /// Pagesa është rimbursuar
        /// </summary>
        Refunded = 5
    }
}