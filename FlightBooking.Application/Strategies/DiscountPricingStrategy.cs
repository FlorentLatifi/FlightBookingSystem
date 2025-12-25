using FlightBooking.Application.Strategies.Pricing;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Application.Strategies.Pricing
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

        public Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            // Fillimisht llogarit çmimin standard
            var standardStrategy = new StandardPricingStrategy();
            var standardPrice = standardStrategy.CalculatePrice(flight, seatClass, numberOfSeats);

            // Apliko zbritjen 10%
            var discountAmount = standardPrice.Multiply(DiscountPercentage);
            var finalPrice = standardPrice - discountAmount;

            return finalPrice;
        }

        public string GetDescription()
        {
            return "Discount pricing with 10% off on all seat classes";
        }
    }
}
