using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies.Pricing
{
    public interface IPricingStrategy
    {
        string StrategyName { get; }
        Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats);
        string GetDescription();
    }
}
