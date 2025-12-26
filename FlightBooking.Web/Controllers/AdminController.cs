using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// Controller për admin panel
    /// NOTE: Authorization është disabled për demo purposes
    /// Për production, aktivizo [Authorize(Roles = "Admin")] dhe implemento Identity
    /// </summary>
    // [Authorize(Roles = "Admin")] // ❌ DISABLED - Enable when Identity is configured
    public class AdminController : Controller
    {
        private readonly Application.Interfaces.Repositories.IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            Application.Interfaces.Repositories.IFlightRepository flightRepository,
            IReservationRepository reservationRepository,
            IPassengerRepository passengerRepository,
            IPaymentRepository paymentRepository,
            ILogger<AdminController> logger)
        {
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _passengerRepository = passengerRepository ?? throw new ArgumentNullException(nameof(passengerRepository));
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {
            return RedirectToAction("Flights");
        }

        public async Task<IActionResult> Flights()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();

                // Calculate statistics
                ViewBag.Stats = new
                {
                    TotalFlights = flights.Count(),
                    AvailableSeats = flights.Sum(f => f.AvailableSeats),
                    TotalRevenue = 0m // Will be calculated from reservations
                };

                return View(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading flights in admin panel");
                return View("Error");
            }
        }

        public async Task<IActionResult> Reservations()
        {
            try
            {
                var reservations = await _reservationRepository.GetAllAsync();
                return View(reservations.OrderByDescending(r => r.ReservationDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading reservations in admin panel");
                return View("Error");
            }
        }
    }
}