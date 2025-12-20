using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Enums
{
    /// <summary>
    /// Klasat e ulëseve në fluturim
    /// </summary>
    public enum SeatClass
    {
        /// <summary>
        /// Klasa ekonomike - më e lira
        /// </summary>
        Economy = 1,

        /// <summary>
        /// Klasa premium economy - më komforte se Economy
        /// </summary>
        PremiumEconomy = 2,

        /// <summary>
        /// Klasa e biznesit - më e shtrenjtë, shumë komforte
        /// </summary>
        Business = 3,

        /// <summary>
        /// Klasa first class - më e shtrenjta dhe luksozja
        /// </summary>
        FirstClass = 4
    }
}
