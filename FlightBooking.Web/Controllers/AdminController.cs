using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly Application.Interfaces.Repositories.IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            Application.Interfaces.Repositories.IFlightRepository flightRepository,
            IReservationRepository reservationRepository,
            ILogger<AdminController> logger)
        {
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
            _reservationRepository = reservationRepository ?? throw new ArgumentNullException(nameof(reservationRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // SAFE: Nuk crash-on asnjëherë
        public async Task<IActionResult> Index()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();
                var reservations = await _reservationRepository.GetAllAsync();

                ViewBag.Stats = new
                {
                    TotalFlights = flights?.Count() ?? 0,
                    AvailableSeats = flights?.Sum(f => f.AvailableSeats) ?? 0,
                    TotalRevenue = reservations?
                        .Where(r => r.Status == ReservationStatus.Confirmed)
                        .Sum(r => r.TotalPrice) ?? 0m
                };

                ViewBag.HasData = flights?.Any() ?? false;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ ERROR në Admin Dashboard");

                // Safe fallback
                ViewBag.Stats = new
                {
                    TotalFlights = 0,
                    AvailableSeats = 0,
                    TotalRevenue = 0m
                };
                ViewBag.HasData = false;
                ViewBag.ErrorMessage = ex.Message;

                return View();
            }
        }

        // SAFE: Nuk crash-on asnjëherë
        public async Task<IActionResult> Flights()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();
                return View(flights ?? new List<FlightBooking.Domain.Entities.Flight>());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ ERROR në Flights list");
                ViewBag.ErrorMessage = $"Error loading flights: {ex.Message}";
                return View(new List<FlightBooking.Domain.Entities.Flight>());
            }
        }

        // SAFE: Nuk crash-on asnjëherë
        public async Task<IActionResult> Reservations()
        {
            try
            {
                var reservations = await _reservationRepository.GetAllAsync();

                var model = reservations?
                    .OrderByDescending(r => r.ReservationDate)
                    .ToList()
                    ?? new List<FlightBooking.Domain.Entities.Reservation>();

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ ERROR në Reservations list");
                ViewBag.ErrorMessage = $"Error loading reservations: {ex.Message}";
                return View(new List<FlightBooking.Domain.Entities.Reservation>());
            }
        }

    }
}