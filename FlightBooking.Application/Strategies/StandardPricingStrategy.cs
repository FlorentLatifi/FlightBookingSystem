using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies
{
    /// <summary>
    /// Strategjia standarde e llogaritjes së çmimit
    /// Aplikon multiplikatorë bazuar në klasën e ulëses
    /// </summary>
    public class StandardPricingStrategy : IPricingStrategy
    {
        public string StrategyName => "Standard Pricing";

        public decimal CalculatePrice(decimal basePrice, SeatClass seatClass)
        {
            // Multiplikatorët për çdo klasë ulëse
            decimal multiplier = seatClass switch
            {
                SeatClass.Economy => 1.0m,          // 100% e çmimit bazë
                SeatClass.PremiumEconomy => 1.5m,   // 150% e çmimit bazë
                SeatClass.Business => 2.5m,         // 250% e çmimit bazë
                SeatClass.FirstClass => 4.0m,       // 400% e çmimit bazë
                _ => 1.0m
            };

            return basePrice * multiplier;
        }
    }
}
