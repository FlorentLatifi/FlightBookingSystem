using System;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies.Pricing
{
    public class LastMinutePricingStrategy : IPricingStrategy
    {
        private const decimal SurchargePercentage = 0.25m;
        private readonly StandardPricingStrategy _standardPricing;

        public string StrategyName => "Last Minute";

        public LastMinutePricingStrategy()
        {
            _standardPricing = new StandardPricingStrategy();
        }

        public Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            var daysUntilDeparture = (flight.DepartureTime - DateTime.UtcNow).TotalDays;

            var standardPrice = _standardPricing.CalculatePrice(flight, seatClass, numberOfSeats);

            if (daysUntilDeparture < 3)
            {
                return standardPrice * (1 + SurchargePercentage);
            }

            return standardPrice;
        }

        public string GetDescription()
        {
            return $"Last minute booking: {SurchargePercentage:P0} surcharge if booked less than 3 days before departure";
        }
    }
}