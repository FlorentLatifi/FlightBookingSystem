using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies
{
    /// <summary>
    /// Strategjia e llogaritjes së çmimit sipas sezonit
    /// Çmimet rriten gjatë sezonit të lartë (verë, festat)
    /// </summary>
    public class SeasonalPricingStrategy : IPricingStrategy
    {
        public string StrategyName => "Seasonal Pricing";

        public decimal CalculatePrice(decimal basePrice, SeatClass seatClass)
        {
            var standardStrategy = new StandardPricingStrategy();
            var standardPrice = standardStrategy.CalculatePrice(basePrice, seatClass);

            // Kontrollo nëse është sezon i lartë
            var currentMonth = DateTime.Now.Month;
            var isHighSeason = IsHighSeason(currentMonth);

            if (isHighSeason)
            {
                // Rritje 20% gjatë sezonit të lartë
                return standardPrice * 1.20m;
            }

            return standardPrice;
        }

        /// <summary>
        /// Kontrollon nëse muaji aktual është sezon i lartë
        /// </summary>
        private bool IsHighSeason(int month)
        {
            // Sezoni i lartë: Qershor-Gusht (verë) dhe Dhjetor (festat)
            return month >= 6 && month <= 8 || month == 12;
        }
    }
}
