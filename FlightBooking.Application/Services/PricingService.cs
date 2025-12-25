using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Application.Strategies.Pricing;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Services
{
    // FlightBooking.Application/Services/PricingService.cs
    public class PricingService
    {
        private readonly Dictionary<string, IPricingStrategy> _strategies;
        private IPricingStrategy _currentStrategy;

        public PricingService()
        {
            _strategies = new Dictionary<string, IPricingStrategy>
            {
                { "standard", new StandardPricingStrategy() },
                { "discount", new DiscountPricingStrategy() }
            };

            _currentStrategy = _strategies["standard"]; // Default
        }

        public void SetStrategy(string strategyKey)
        {
            if (_strategies.ContainsKey(strategyKey.ToLower()))
            {
                _currentStrategy = _strategies[strategyKey.ToLower()];
            }
            else
            {
                throw new ArgumentException($"Unknown pricing strategy: {strategyKey}");
            }
        }

        public void SetStrategy(IPricingStrategy strategy)
        {
            _currentStrategy = strategy ?? throw new ArgumentNullException(nameof(strategy));
        }

        public Money CalculatePrice(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            return _currentStrategy.CalculatePrice(flight, seatClass, numberOfSeats);
        }

        public string GetCurrentStrategyName()
        {
            return _currentStrategy.StrategyName;
        }

        public string GetCurrentStrategyDescription()
        {
            return _currentStrategy.GetDescription();
        }

        public IPricingStrategy GetBestStrategy(Flight flight, int numberOfSeats)
        {
            // Automatically select the best pricing strategy
            var daysUntilDeparture = (flight.DepartureTime - DateTime.UtcNow).TotalDays;

            if (numberOfSeats >= 5)
                return _strategies.ContainsKey("group") ? _strategies["group"] : _strategies["standard"];
            else if (daysUntilDeparture > 30)
                return _strategies.ContainsKey("earlybird") ? _strategies["earlybird"] : _strategies["standard"];
            else if (daysUntilDeparture < 3)
                return _strategies.ContainsKey("lastminute") ? _strategies["lastminute"] : _strategies["standard"];
            else
                return _strategies["standard"];
        }

        public Dictionary<string, Money> GetAllPriceComparisons(Flight flight, SeatClass seatClass, int numberOfSeats)
        {
            var prices = new Dictionary<string, Money>();

            foreach (var strategy in _strategies.Values)
            {
                prices[strategy.StrategyName] = strategy.CalculatePrice(flight, seatClass, numberOfSeats);
            }

            return prices;
        }
    }
}
