using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies
{
    /// <summary>
    /// Interface për strategjitë e llogaritjes së çmimit
    /// STRATEGY PATTERN - Lejon ndryshimin e algoritmit të llogaritjes në runtime
    /// </summary>
    public interface IPricingStrategy
    {
        /// <summary>
        /// Llogarit çmimin total bazuar në çmimin bazë dhe klasën e ulëses
        /// </summary>
        /// <param name="basePrice">Çmimi bazë i fluturimit</param>
        /// <param name="seatClass">Klasa e ulëses së zgjedhur</param>
        /// <returns>Çmimi total i llogaritur</returns>
        decimal CalculatePrice(decimal basePrice, SeatClass seatClass);

        /// <summary>
        /// Emri i strategjisë (për logging dhe debugging)
        /// </summary>
        string StrategyName { get; }
    }
}
