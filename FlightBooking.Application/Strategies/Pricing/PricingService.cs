using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Strategies.Pricing
{
    public class PricingService
    {
        private readonly Dictionary<string, IPricingStrategy> _strategies;
        private IPricingStrategy _currentStrategy;

        public PricingService()
        {
            _strategies = new Dictionary<string, IPricingStrategy>
            {
                { "standard", new StandardPricingStrategy() },
                { "earlybird", new EarlyBirdPricingStrategy() },
                { "lastminute", new LastMinutePricingStrategy() },
                { "group", new GroupPricingStrategy() },
                { "seasonal", new SeasonalPricingStrategy() }
            };

            _currentStrategy = _strategies["standard"];
        }

        public void SetStrategy(string strategyKey)
        {
            if (_strategies.TryGetValue(strategyKey.ToLower(), out var strategy))
            {
                _currentStrategy = strategy;
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

        public string GetCurrentStrategyName() => _currentStrategy.StrategyName;

        public string GetCurrentStrategyDescription() => _currentStrategy.GetDescription();

        public IPricingStrategy GetBestStrategy(Flight flight, int numberOfSeats)
        {
            var daysUntilDeparture = (flight.DepartureTime - DateTime.UtcNow).TotalDays;

            if (numberOfSeats >= 5)
                return _strategies["group"];
            else if (daysUntilDeparture > 30)
                return _strategies["earlybird"];
            else if (daysUntilDeparture < 3)
                return _strategies["lastminute"];
            else
                return _strategies["standard"];
        }

        public List<string> GetAllStrategyNames()
        {
            return _strategies.Keys.ToList();
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