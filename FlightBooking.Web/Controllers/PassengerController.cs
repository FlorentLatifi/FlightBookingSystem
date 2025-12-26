using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.DTOs;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// Controller për panelin e pasagjerit
    /// Shfaq rezervimet dhe profilin
    /// </summary>
    public class PassengerController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly ILogger<PassengerController> _logger;

        public PassengerController(
            IReservationRepository reservationRepository,
            IPassengerRepository passengerRepository,
            ILogger<PassengerController> logger)
        {
            _reservationRepository = reservationRepository;
            _passengerRepository = passengerRepository;
            _logger = logger;
        }

        /// <summary>
        /// GET: /Passenger/MyBookings
        /// Shfaq rezervimet e pasagjerit
        /// Për demo, përdorim email nga query string
        /// </summary>
        public async Task<IActionResult> MyBookings(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Message = "Ju lutem fut email për të parë rezervimet";
                return View(new List<Domain.Entities.Reservation>());
            }

            try
            {
                var passenger = await _passengerRepository.GetByEmailAsync(email);

                if (passenger == null)
                {
                    ViewBag.Message = $"Nuk u gjet asnjë pasagjer me email: {email}";
                    return View(new List<Domain.Entities.Reservation>());
                }

                var reservations = await _reservationRepository.GetByPassengerIdAsync(passenger.Id);

                ViewBag.PassengerName = passenger.FullName;
                ViewBag.PassengerEmail = passenger.Email;

                return View(reservations.OrderByDescending(r => r.ReservationDate));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë ngarkimit të rezervimeve për {Email}", email);
                ViewBag.Message = "Ndodhi një gabim gjatë ngarkimit të rezervimeve";
                return View(new List<Domain.Entities.Reservation>());
            }
        }

        /// <summary>
        /// GET: /Passenger/Profile
        /// Shfaq profilin e pasagjerit
        /// </summary>
        public async Task<IActionResult> Profile(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                ViewBag.Message = "Ju lutem fut email për të parë profilin";
                return View();
            }

            try
            {
                var passenger = await _passengerRepository.GetByEmailAsync(email);

                if (passenger == null)
                {
                    ViewBag.Message = $"Nuk u gjet asnjë pasagjer me email: {email}";
                    return View();
                }

                // Merr dhe statistika për këtë pasagjer
                var reservations = await _reservationRepository.GetByPassengerIdAsync(passenger.Id);

                ViewBag.TotalReservations = reservations.Count();
                ViewBag.ConfirmedReservations = reservations.Count(r => r.Status == Domain.Enums.ReservationStatus.Confirmed);
                ViewBag.TotalSpent = reservations.Where(r => r.Status == Domain.Enums.ReservationStatus.Confirmed)
                                                .Sum(r => r.TotalPrice);

                return View(passenger);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë ngarkimit të profilit për {Email}", email);
                ViewBag.Message = "Ndodhi një gabim gjatë ngarkimit të profilit";
                return View();
            }
        }
    }
}
