using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies
{
    /// <summary>
    /// Strategjia e llogaritjes së çmimit me zbritje
    /// Aplikon zbritje 10% për të gjitha klasat
    /// Përdoret për promovime speciale, black friday, etj.
    /// </summary>
    public class DiscountPricingStrategy : IPricingStrategy
    {
        private const decimal DiscountPercentage = 0.10m; // 10% zbritje

        public string StrategyName => "Discount Pricing (10% OFF)";

        public decimal CalculatePrice(decimal basePrice, SeatClass seatClass)
        {
            // Fillimisht llogarit çmimin standard
            var standardStrategy = new StandardPricingStrategy();
            var standardPrice = standardStrategy.CalculatePrice(basePrice, seatClass);

            // Apliko zbritjen 10%
            var discountAmount = standardPrice * DiscountPercentage;
            var finalPrice = standardPrice - discountAmount;

            return finalPrice;
        }
    }
}
