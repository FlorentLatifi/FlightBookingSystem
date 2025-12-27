using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.DTOs;
using FlightBooking.Application.Validators;
using FlightBooking.Application.Mappers;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// Controller për menaxhimin e fluturimeve
    /// Demonstron MVC Pattern dhe Repository Pattern
    /// </summary>
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly ILogger<FlightController> _logger;

        public FlightController(
            IFlightService flightService,
            ILogger<FlightController> logger)
        {
            _flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// GET: /Flight/Search
        /// Shfaq search form
        /// </summary>
        [HttpGet]
        public IActionResult Search()
        {
            return View(new FlightSearchDto
            {
                DepartureDate = DateTime.Now.AddDays(7)
            });
        }

        /// <summary>
        /// POST: /Flight/Search
        /// Kërkon fluturime dhe shfaq rezultatet
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(FlightSearchDto dto)
        {
            _logger.LogInformation("Search request: {From} -> {To} on {Date}",
                dto.DepartureAirport, dto.ArrivalAirport, dto.DepartureDate);

            // Validation
            var validator = new FlightSearchValidator();
            var validationResult = validator.Validate(dto);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.GetAllErrors())
                {
                    ModelState.AddModelError(string.Empty, error);
                }
                return View(dto);
            }

            try
            {
                // Search flights
                var flights = await _flightService.SearchAvailableFlightsAsync(
                    dto.DepartureAirport,
                    dto.ArrivalAirport,
                    dto.DepartureDate
                );

                _logger.LogInformation("Found {Count} flights", flights.Count);

                // Convert to DTOs
                var flightDtos = flights.Select(f => f.ToDto()).ToList();

                // Save search criteria
                TempData["SearchCriteria"] = $"{dto.DepartureAirport} → {dto.ArrivalAirport} | {dto.DepartureDate:dd/MM/yyyy}";

                return View("SearchResults", flightDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching flights");
                ModelState.AddModelError(string.Empty, "An error occurred while searching. Please try again.");
                return View(dto);
            }
        }

        /// <summary>
        /// GET: /Flight/Details/5
        /// Shfaq detajet e një fluturimi
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var flight = await _flightService.GetFlightDetailsAsync(id);

                if (flight == null)
                {
                    _logger.LogWarning("Flight {Id} not found", id);
                    return NotFound();
                }

                return View(flight.ToDto());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading flight details");
                return StatusCode(500);
            }
        }
    }
}