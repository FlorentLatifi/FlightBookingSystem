using FlightBooking.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// DESIGN PATTERN: MVC Pattern + Repository Pattern
    /// Admin panel për menaxhimin e fluturimeve dhe rezervimeve
    /// Demonstron CRUD operations dhe data management
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IReservationService _reservationService;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            IFlightService flightService,
            IReservationService reservationService,
            ILogger<AdminController> logger)
        {
            _flightService = flightService;
            _reservationService = reservationService;
            _logger = logger;
        }

        // GET: /Admin
        public async Task<IActionResult> Index()
        {
            var flights = await _flightService.SearchAvailableFlightsAsync("", "", DateTime.Now);
            // Simple statistics
            var stats = new
            {
                TotalFlights = flights.Count,
                AvailableSeats = flights.Sum(f => f.AvailableSeats),
                TotalRevenue = 0m // Calculate from reservations
            };

            ViewBag.Stats = stats;
            return View();
        }

        // GET: /Admin/Flights
        public async Task<IActionResult> Flights()
        {
            // Get all flights for management
            return View();
        }

        // GET: /Admin/Reservations
        public async Task<IActionResult> Reservations()
        {
            // Get all reservations
            return View();
        }
    }
}
