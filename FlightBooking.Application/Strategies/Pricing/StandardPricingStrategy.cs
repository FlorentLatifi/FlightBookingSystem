using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies.Pricing
{
    public class StandardPricingStrategy : IPricingStrategy
    {
        public string StrategyName => "Standard";

        public Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            decimal classMultiplier = seatClass switch
            {
                SeatClass.Economy => 1.0m,
                SeatClass.PremiumEconomy => 1.5m,
                SeatClass.Business => 2.5m,
                SeatClass.FirstClass => 4.0m,
                _ => 1.0m
            };

            decimal totalAmount = flight.BasePriceAmount * classMultiplier * numberOfSeats;
            return new Money(totalAmount, flight.BasePriceCurrency);
        }

        public string GetDescription()
        {
            return "Standard pricing based on seat class";
        }
    }
}