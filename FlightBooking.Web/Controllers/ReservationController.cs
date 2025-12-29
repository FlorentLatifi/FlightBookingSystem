using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FlightBooking.Application.DTOs;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// FIXED: All null reference warnings resolved
    /// Controller for the complete booking flow
    /// </summary>
    public class ReservationController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly IReservationService _reservationService;
        private readonly IPaymentService _paymentService;
        private readonly IPassengerRepository _passengerRepository;
        private readonly INotificationService _notificationService;
        private readonly ILogger<ReservationController> _logger;

        public ReservationController(
            IFlightService flightService,
            IReservationService reservationService,
            IPaymentService paymentService,
            IPassengerRepository passengerRepository,
            INotificationService notificationService,
            ILogger<ReservationController> logger)
        {
            _flightService = flightService ?? throw new ArgumentNullException(nameof(flightService));
            _reservationService = reservationService ?? throw new ArgumentNullException(nameof(reservationService));
            _paymentService = paymentService ?? throw new ArgumentNullException(nameof(paymentService));
            _passengerRepository = passengerRepository ?? throw new ArgumentNullException(nameof(passengerRepository));
            _notificationService = notificationService ?? throw new ArgumentNullException(nameof(notificationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> Create(int flightId)
        {
            _logger.LogInformation("Starting booking process for flight ID: {FlightId}", flightId);

            try
            {
                var flight = await _flightService.GetFlightDetailsAsync(flightId);

                if (flight == null)
                {
                    _logger.LogWarning("Flight {FlightId} not found", flightId);
                    TempData["ErrorMessage"] = "Flight not found.";
                    return RedirectToAction("Index", "Home");
                }

                if (!flight.CanBeBooked())
                {
                    _logger.LogWarning("Flight {FlightNumber} cannot be booked", flight.FlightNumber);
                    TempData["ErrorMessage"] = "This flight cannot be booked.";
                    return RedirectToAction("Index", "Home");
                }

                var createDto = new CreateReservationDto
                {
                    FlightId = flightId,
                    SeatClass = SeatClass.Economy,
                    Passenger = new PassengerDto
                    {
                        DateOfBirth = DateTime.Now.AddYears(-25)
                    },
                    PaymentDetails = new PaymentDetailsDto
                    {
                        PaymentMethod = "Credit Card"
                    }
                };

                ViewBag.Flight = new FlightDto
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    Airline = flight.Airline,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    BasePrice = flight.BasePriceAmount,
                    AvailableSeats = flight.AvailableSeats,
                    Status = flight.Status,
                    DurationMinutes = flight.DurationMinutes
                };

                return View(createDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error opening booking form");
                TempData["ErrorMessage"] = "An error occurred. Please try again.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservationDto createDto)
        {
            _logger.LogInformation("Processing reservation for flight ID: {FlightId}", createDto.FlightId);

            if (!ModelState.IsValid)
            {
                var flight = await _flightService.GetFlightDetailsAsync(createDto.FlightId);

                if (flight == null)
                {
                    _logger.LogWarning("Flight not found during validation");
                    TempData["ErrorMessage"] = "Flight not found.";
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Flight = new FlightDto
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    Airline = flight.Airline,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    BasePrice = flight.BasePriceAmount,
                    AvailableSeats = flight.AvailableSeats,
                    Status = flight.Status,
                    DurationMinutes = flight.DurationMinutes
                };

                return View(createDto);
            }

            try
            {
                // Create or get passenger
                Passenger passenger;
                var existingPassenger = await _passengerRepository.GetByEmailAsync(createDto.Passenger.Email);

                if (existingPassenger != null)
                {
                    _logger.LogInformation("Existing passenger: {Email}", createDto.Passenger.Email);
                    passenger = existingPassenger;
                }
                else
                {
                    _logger.LogInformation("Creating new passenger: {Email}", createDto.Passenger.Email);
                    passenger = new Passenger
                    {
                        FirstName = createDto.Passenger.FirstName,
                        LastName = createDto.Passenger.LastName,
                        Email = createDto.Passenger.Email,
                        PhoneNumber = createDto.Passenger.PhoneNumber,
                        PassportNumber = createDto.Passenger.PassportNumber,
                        DateOfBirth = createDto.Passenger.DateOfBirth,
                        Nationality = createDto.Passenger.Nationality
                    };

                    await _passengerRepository.AddAsync(passenger);

                    if (passenger.Id == 0)
                    {
                        throw new InvalidOperationException("Passenger ID not generated after save");
                    }

                    _logger.LogInformation("Passenger created with ID: {PassengerId}", passenger.Id);
                }

                // Create reservation
                var reservation = await _reservationService.CreateReservationAsync(
                    createDto.FlightId,
                    passenger.Id,
                    createDto.SeatClass);

                _logger.LogInformation("Reservation created: {Code}", reservation.ReservationCode);

                // PARALLEL PROCESSING (as in exam!)
                _logger.LogInformation("🔥 [PARALLEL PROCESSING] Starting parallel processing...");
                Console.WriteLine("\n🔥 [PARALLEL PROCESSING] Starting parallel processing...\n");

                var paymentTask = _paymentService.ProcessPaymentAsync(
                    reservation.Id,
                    reservation.TotalPrice,
                    createDto.PaymentDetails.PaymentMethod,
                    createDto.PaymentDetails.CardNumber,
                    createDto.PaymentDetails.CardHolderName,
                    createDto.PaymentDetails.CVV,
                    createDto.PaymentDetails.ExpiryDate);

                var notificationPrepTask = _notificationService.PrepareNotificationsAsync(reservation);

                await Task.WhenAll(paymentTask, notificationPrepTask);

                _logger.LogInformation("✅ [PARALLEL PROCESSING] Both tasks completed!");
                Console.WriteLine("\n🎉 [PARALLEL PROCESSING] Both tasks completed!\n");

                var payment = await paymentTask;

                if (payment.IsSuccessful)
                {
                    _logger.LogInformation("✅ Payment successful: {TransactionId}", payment.TransactionId);

                    await _reservationService.ConfirmReservationAsync(reservation.Id);

                    _logger.LogInformation("🔥 [OBSERVER PATTERN] Sending prepared notifications...");
                    await _notificationService.SendPreparedNotificationsAsync();

                    return RedirectToAction("Success", new { reservationCode = reservation.ReservationCode });
                }
                else
                {
                    _logger.LogWarning("Payment failed: {TransactionId}", payment.TransactionId);
                    await _reservationService.CancelReservationAsync(reservation.Id);

                    TempData["ErrorMessage"] = $"Payment failed: {payment.PaymentGatewayResponse}";
                    return RedirectToAction("Failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "❌ CRITICAL ERROR during reservation processing");
                var innerMessage = ex.InnerException?.Message ?? ex.Message;
                TempData["ErrorMessage"] = $"An error occurred: {innerMessage}";
                return RedirectToAction("Failed");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Success(string reservationCode)
        {
            _logger.LogInformation("Showing success page for reservation: {Code}", reservationCode);

            try
            {
                var reservation = await _reservationService.GetReservationByCodeAsync(reservationCode);

                if (reservation == null)
                {
                    _logger.LogWarning("Reservation {Code} not found", reservationCode);
                    TempData["ErrorMessage"] = "Reservation not found.";
                    return RedirectToAction("Index", "Home");
                }

                // ✅ NULL CHECKS ADDED
                if (reservation.Flight == null)
                {
                    _logger.LogError("Reservation {Code} has null Flight", reservationCode);
                    TempData["ErrorMessage"] = "Flight information is missing.";
                    return RedirectToAction("Index", "Home");
                }

                if (reservation.Passenger == null)
                {
                    _logger.LogError("Reservation {Code} has null Passenger", reservationCode);
                    TempData["ErrorMessage"] = "Passenger information is missing.";
                    return RedirectToAction("Index", "Home");
                }

                var reservationDto = new ReservationDto
                {
                    Id = reservation.Id,
                    ReservationCode = reservation.ReservationCode,
                    Flight = new FlightDto
                    {
                        FlightNumber = reservation.Flight.FlightNumber,
                        Airline = reservation.Flight.Airline,
                        DepartureAirport = reservation.Flight.DepartureAirport,
                        ArrivalAirport = reservation.Flight.ArrivalAirport,
                        DepartureTime = reservation.Flight.DepartureTime,
                        ArrivalTime = reservation.Flight.ArrivalTime
                    },
                    Passenger = new PassengerDto
                    {
                        FirstName = reservation.Passenger.FirstName,
                        LastName = reservation.Passenger.LastName,
                        Email = reservation.Passenger.Email
                    },
                    SeatClass = reservation.SeatClass,
                    SeatNumber = reservation.SeatNumber,
                    TotalPrice = reservation.TotalPrice,
                    Status = reservation.Status,
                    ReservationDate = reservation.ReservationDate
                };

                return View(reservationDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error showing success page");
                TempData["ErrorMessage"] = "An error occurred.";
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Failed()
        {
            return View();
        }
    }
}