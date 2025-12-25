using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies.Pricing
{
    public class GroupPricingStrategy : IPricingStrategy
    {
        private const int MinimumGroupSize = 5;
        private const decimal GroupDiscountPercentage = 0.10m;
        private readonly StandardPricingStrategy _standardPricing;

        public string StrategyName => "Group Booking";

        public GroupPricingStrategy()
        {
            _standardPricing = new StandardPricingStrategy();
        }

        public Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            var standardPrice = _standardPricing.CalculatePrice(flight, seatClass, numberOfSeats);

            if (numberOfSeats >= MinimumGroupSize)
            {
                return standardPrice * (1 - GroupDiscountPercentage);
            }

            return standardPrice;
        }

        public string GetDescription()
        {
            return $"Group discount: {GroupDiscountPercentage:P0} off when booking {MinimumGroupSize}+ seats";
        }
    }
}
