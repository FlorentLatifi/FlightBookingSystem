using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// Controller për admin panel
    /// Shfaq statistika dhe menaxhim të fluturimeve/rezervimeve
    /// </summary>
    public class AdminController : Controller
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<AdminController> _logger;

        public AdminController(
            IFlightRepository flightRepository,
            IReservationRepository reservationRepository,
            IPassengerRepository passengerRepository,
            IPaymentRepository paymentRepository,
            ILogger<AdminController> logger)
        {
            _flightRepository = flightRepository;
            _reservationRepository = reservationRepository;
            _passengerRepository = passengerRepository;
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        /// <summary>
        /// GET: /Admin/Index
        /// Dashboard kryesore
        /// </summary>
        public IActionResult Index()
        {
            return RedirectToAction("Statistics");
        }

        /// <summary>
        /// GET: /Admin/Statistics
        /// Shfaq statistika të sistemit
        /// </summary>
        public async Task<IActionResult> Statistics()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();
                var reservations = await _reservationRepository.GetAllAsync();
                var passengers = await _passengerRepository.GetAllAsync();
                var payments = await _paymentRepository.GetAllAsync();

                // Llogarit statistikat
                var stats = new
                {
                    TotalFlights = flights.Count(),
                    TotalReservations = reservations.Count(),
                    TotalPassengers = passengers.Count(),
                    TotalRevenue = payments.Where(p => p.Status == PaymentStatus.Completed)
                                          .Sum(p => p.Amount),

                    // Rezervimet sipas statusit
                    PendingReservations = reservations.Count(r => r.Status == ReservationStatus.Pending),
                    ConfirmedReservations = reservations.Count(r => r.Status == ReservationStatus.Confirmed),
                    CancelledReservations = reservations.Count(r => r.Status == ReservationStatus.Cancelled),

                    // Fluturimet sipas statusit
                    ScheduledFlights = flights.Count(f => f.Status == FlightStatus.Scheduled),
                    DepartedFlights = flights.Count(f => f.Status == FlightStatus.Departed),

                    // Top destinacionet
                    TopDestinations = flights.GroupBy(f => f.ArrivalAirport)
                                           .Select(g => new { Airport = g.Key, Count = g.Count() })
                                           .OrderByDescending(x => x.Count)
                                           .Take(5)
                                           .ToList(),

                    // Pagesat e fundit
                    RecentPayments = payments.OrderByDescending(p => p.PaymentDate)
                                            .Take(10)
                                            .ToList()
                };

                ViewBag.Statistics = stats;
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë ngarkimit të statistikave");
                return View("Error");
            }
        }

        /// <summary>
        /// GET: /Admin/Flights
        /// Lista e fluturimeve
        /// </summary>
        public async Task<IActionResult> Flights()
        {
            try
            {
                var flights = await _flightRepository.GetAllAsync();
                return View(flights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë ngarkimit të fluturimeve");
                return View("Error");
            }
        }

        /// <summary>
        /// GET: /Admin/Reservations
        /// Lista e rezervimeve
        /// </summary>
        public async Task<IActionResult> Reservations()
        {
            try
            {
                var reservations = await _reservationRepository.GetAllAsync();
                return View(reservations.OrderByDescending(r => r.ReservationDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë ngarkimit të rezervimeve");
                return View("Error");
            }
        }
    }
}