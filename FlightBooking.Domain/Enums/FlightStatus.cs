using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Enums
{
    /// <summary>
    /// Statuset e mundshme të një fluturimi
    /// </summary>
    public enum FlightStatus
    {
        /// <summary>
        /// Fluturimi është i planifikuar por ende nuk ka filluar
        /// </summary>
        Scheduled = 1,

        /// <summary>
        /// Fluturimi është duke u bërë boarding (hipje në aeroplan)
        /// </summary>
        Boarding = 2,

        /// <summary>
        /// Fluturimi ka nisur
        /// </summary>
        Departed = 3,

        /// <summary>
        /// Fluturimi ka mbërritur në destinacion
        /// </summary>
        Arrived = 4,

        /// <summary>
        /// Fluturimi është anuluar
        /// </summary>
        Cancelled = 5,

        /// <summary>
        /// Fluturimi është vonuar
        /// </summary>
        Delayed = 6
    }
}
