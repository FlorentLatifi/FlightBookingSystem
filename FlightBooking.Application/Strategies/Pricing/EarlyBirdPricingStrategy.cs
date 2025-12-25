using System;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies.Pricing
{
    public class EarlyBirdPricingStrategy : IPricingStrategy
    {
        private const decimal DiscountPercentage = 0.15m;
        private readonly IPricingStrategy _standardPricing;

        public string StrategyName => "Early Bird";

        public EarlyBirdPricingStrategy()
        {
            _standardPricing = new StandardPricingStrategy();
        }

        public Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            var daysUntilDeparture = (flight.DepartureTime - DateTime.UtcNow).TotalDays;

            var standardPrice = _standardPricing.CalculatePrice(flight, seatClass, numberOfSeats);

            if (daysUntilDeparture > 30)
            {
                return standardPrice * (1 - DiscountPercentage);
            }

            return standardPrice;
        }

        public string GetDescription()
        {
            return $"Early bird discount: {DiscountPercentage:P0} off if booked 30+ days in advance";
        }
    }
}