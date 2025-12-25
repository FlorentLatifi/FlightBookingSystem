using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Services;

namespace FlightBooking.Application.RefactoringExamples.AfterRefactoring
{
    // Company.Application/RefactoringExamples/AfterRefactoring/FlightSearchController.cs

    /// <summary>
    /// AFTER REFACTORING - Improvements:
    /// 1. Extract Method - validation extracted
    /// 2. Single Responsibility - Controller only handles HTTP
    /// 3. Strategy Pattern - pricing logic extracted
    /// 4. Repository Pattern - data access extracted
    /// 5. Value Objects - Money, SearchCriteria
    /// </summary>
    public class FlightSearchController_New : Controller
    {
        private readonly IFlightService _flightService;

        public FlightSearchController_New(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpPost]
        public async Task<IActionResult> Search(FlightSearchRequest request)
        {
            // Validation handled by DataAnnotations and ModelState
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            // Create Value Object
            var searchCriteria = new FlightSearchCriteria(
                request.Origin,
                request.Destination,
                request.DepartureDate,
                request.Passengers);

            // Delegate to service
            var flights = await _flightService.SearchFlightsAsync(searchCriteria);

            return View(flights);
        }
    }

    // Refactored: DTOs with validation
    public class FlightSearchRequest
    {
        [Required(ErrorMessage = "Origin is required")]
        public string Origin { get; set; }

        [Required(ErrorMessage = "Destination is required")]
        public string Destination { get; set; }

        [Required(ErrorMessage = "Date is required")]
        [DataType(DataType.Date)]
        [FutureDate(ErrorMessage = "Date must be in the future")]
        public DateTime DepartureDate { get; set; }

        [Range(1, 10, ErrorMessage = "Passengers must be between 1 and 10")]
        public int Passengers { get; set; }
    }

    // Refactored: Value Object
    public class FlightSearchCriteria
    {
        public string Origin { get; }
        public string Destination { get; }
        public DateTime DepartureDate { get; }
        public int NumberOfPassengers { get; }

        public FlightSearchCriteria(string origin, string destination, DateTime departureDate, int passengers)
        {
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));
            DepartureDate = departureDate;
            NumberOfPassengers = passengers;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Origin))
                throw new ArgumentException("Origin cannot be empty");

            if (string.IsNullOrWhiteSpace(Destination))
                throw new ArgumentException("Destination cannot be empty");

            if (DepartureDate < DateTime.Today)
                throw new ArgumentException("Departure date cannot be in the past");

            if (NumberOfPassengers < 1 || NumberOfPassengers > 10)
                throw new ArgumentException("Number of passengers must be between 1 and 10");
        }
    }

    // Refactored: Service handles business logic
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly PricingService _pricingService;

        public FlightService(IFlightRepository flightRepository, PricingService pricingService)
        {
            _flightRepository = flightRepository;
            _pricingService = pricingService;
        }

        public async Task<List<FlightSearchResult>> SearchFlightsAsync(FlightSearchCriteria criteria)
        {
            // Repository handles data access
            var flights = await _flightRepository.SearchAsync(
                criteria.Origin,
                criteria.Destination,
                criteria.DepartureDate);

            // Strategy Pattern handles pricing
            var results = new List<FlightSearchResult>();
            foreach (var flight in flights)
            {
                var bestStrategy = _pricingService.GetBestStrategy(flight, criteria.NumberOfPassengers);
                _pricingService.SetStrategy(bestStrategy);

                var price = _pricingService.CalculatePrice(
                    flight,
                    SeatClass.Economy, // Default
                    criteria.NumberOfPassengers);

                results.Add(new FlightSearchResult
                {
                    Flight = flight,
                    Price = price,
                    PricingStrategy = bestStrategy.StrategyName,
                    AvailableSeats = flight.AvailableSeats
                });
            }

            return results.OrderBy(r => r.Price.Amount).ToList();
        }
    }
}
