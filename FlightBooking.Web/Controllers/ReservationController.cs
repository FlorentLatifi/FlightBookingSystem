using Microsoft.AspNetCore.Mvc;

using FlightBooking.Application.DTOs;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// Controller për procesin e rezervimit
    /// Demonstron flow-in e plotë: Flight → Passenger Info → Payment → Confirmation
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
            _flightService = flightService;
            _reservationService = reservationService;
            _paymentService = paymentService;
            _passengerRepository = passengerRepository;
            _notificationService = notificationService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /Reservation/Create/{flightId}
        /// HAPI 1: Shfaq form për të dhënat e pasagjerit
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Create(int flightId)
        {
            _logger.LogInformation("Duke filluar procesin e rezervimit për fluturimin ID: {FlightId}", flightId);

            try
            {
                var flight = await _flightService.GetFlightDetailsAsync(flightId);

                if (flight == null)
                {
                    _logger.LogWarning("Fluturimi me ID {FlightId} nuk u gjet", flightId);
                    TempData["ErrorMessage"] = "Fluturimi nuk u gjet.";
                    return RedirectToAction("Index", "Home");
                }

                if (!flight.CanBeBooked())
                {
                    _logger.LogWarning("Fluturimi {FlightNumber} nuk mund të rezervohet", flight.FlightNumber);
                    TempData["ErrorMessage"] = "Ky fluturim nuk mund të rezervohet.";
                    return RedirectToAction("Index", "Home");
                }

                // Krijo DTO për form
                var createDto = new CreateReservationDto
                {
                    FlightId = flightId,
                    SeatClass = SeatClass.Economy, // Default
                    Passenger = new PassengerDto
                    {
                        DateOfBirth = DateTime.Now.AddYears(-25) // Default: 25 vjeç
                    },
                    PaymentDetails = new PaymentDetailsDto
                    {
                        PaymentMethod = "Credit Card"
                    }
                };

                // Kaloje flight details në ViewBag për ta shfaqur në form
                ViewBag.Flight = new FlightDto
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    Airline = flight.Airline,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    BasePrice = flight.BasePrice,
                    AvailableSeats = flight.AvailableSeats,
                    Status = flight.Status,
                    DurationMinutes = flight.DurationMinutes
                };

                return View(createDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë hapjes së formës së rezervimit");
                TempData["ErrorMessage"] = "Ndodhi një gabim. Ju lutem provoni përsëri.";
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// POST: /Reservation/Create
        /// HAPI 2: Procesohet rezervimi dhe pagesa NË PARALEL
        /// DEMONSTRON: PARALLEL PROCESSING (si në provim!)
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateReservationDto createDto)
        {
            _logger.LogInformation("Duke procesuar rezervimin për fluturimin ID: {FlightId}", createDto.FlightId);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Të dhënat e formës nuk janë të vlefshme");

                // Rifut flight details në ViewBag
                var flight = await _flightService.GetFlightDetailsAsync(createDto.FlightId);
                ViewBag.Flight = new FlightDto
                {
                    Id = flight.Id,
                    FlightNumber = flight.FlightNumber,
                    Airline = flight.Airline,
                    DepartureAirport = flight.DepartureAirport,
                    ArrivalAirport = flight.ArrivalAirport,
                    DepartureTime = flight.DepartureTime,
                    ArrivalTime = flight.ArrivalTime,
                    BasePrice = flight.BasePrice,
                    AvailableSeats = flight.AvailableSeats,
                    Status = flight.Status,
                    DurationMinutes = flight.DurationMinutes
                };

                return View(createDto);
            }

            try
            {
                // =============================================
                // HAPI 1: KRIJO/MERR PASAGJERIN
                // =============================================
                Passenger passenger;
                var existingPassenger = await _passengerRepository.GetByEmailAsync(createDto.Passenger.Email);

                if (existingPassenger != null)
                {
                    _logger.LogInformation("Pasagjeri ekziston: {Email}", createDto.Passenger.Email);
                    passenger = existingPassenger;
                }
                else
                {
                    _logger.LogInformation("Duke krijuar pasagjer të ri: {Email}", createDto.Passenger.Email);
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
                }

                // =============================================
                // HAPI 2: KRIJO REZERVIMIN
                // =============================================
                var reservation = await _reservationService.CreateReservationAsync(
                    createDto.FlightId,
                    passenger.Id,
                    createDto.SeatClass);

                _logger.LogInformation("Rezervimi u krijua: {Code}", reservation.ReservationCode);

                // =============================================
                // HAPI 3: PROCESIMI PARALLEL
                // 🔥 DEMONSTRON: PARALLEL PROCESSING (si në provim!)
                // Pagesa dhe Njoftimi ndodhin NË TË NJËJTËN KOHË
                // =============================================
                Console.WriteLine("\n🔥🔥🔥 DUKE FILLUAR PROCESIMIN PARALLEL! 🔥🔥🔥\n");

                // Task 1: Proceso pagesën
                var paymentTask = _paymentService.ProcessPaymentAsync(
                    reservation.Id,
                    reservation.TotalPrice,
                    createDto.PaymentDetails.PaymentMethod,
                    createDto.PaymentDetails.CardNumber,
                    createDto.PaymentDetails.CardHolderName,
                    createDto.PaymentDetails.CVV,
                    createDto.PaymentDetails.ExpiryDate);

                // Task 2: Gjenero njoftimin (por ende nuk dërgohet)
                // Në realitet, këtu do të përgatitej njoftimi
                var notificationTask = Task.Run(async () =>
                {
                    Console.WriteLine("[Parallel Task] Njoftimi po përgatitet...");
                    await Task.Delay(500); // Simulon përgatitjen
                    Console.WriteLine("[Parallel Task] Njoftimi u përgatit!");
                });

                // Prit që TË DY task-et të përfundojnë
                await Task.WhenAll(paymentTask, notificationTask);

                Console.WriteLine("\n🎉 PROCESIMI PARALLEL PËRFUNDOI! 🎉\n");

                var payment = await paymentTask;

                // =============================================
                // HAPI 4: KONTROLLO STATUSIN E PAGESËS
                // =============================================
                if (payment.IsSuccessful())
                {
                    _logger.LogInformation("Pagesa u krye me sukses: {TransactionId}", payment.TransactionId);

                    // Konfirmo rezervimin
                    await _reservationService.ConfirmReservationAsync(reservation.Id);

                    // =============================================
                    // HAPI 5: DËRGO NJOFTIME (OBSERVER PATTERN)
                    // Email dhe SMS dërgohen NË PARALEL!
                    // =============================================
                    await _notificationService.SendReservationConfirmationAsync(reservation);
                    await _notificationService.SendPaymentConfirmationAsync(payment);

                    // Redirect te faqja e suksesit
                    return RedirectToAction("Success", new { reservationCode = reservation.ReservationCode });
                }
                else
                {
                    _logger.LogWarning("Pagesa dështoi: {TransactionId}", payment.TransactionId);

                    // Anulo rezervimin nëse pagesa dështoi
                    await _reservationService.CancelReservationAsync(reservation.Id);

                    TempData["ErrorMessage"] = $"Pagesa dështoi: {payment.PaymentGatewayResponse}";
                    return RedirectToAction("Failed");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë procesimit të rezervimit");
                TempData["ErrorMessage"] = $"Ndodhi një gabim: {ex.Message}";
                return RedirectToAction("Failed");
            }
        }

        /// <summary>
        /// GET: /Reservation/Success/{reservationCode}
        /// HAPI 3: Faqja e suksesit
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Success(string reservationCode)
        {
            _logger.LogInformation("Shfaq faqen e suksesit për rezervimin: {Code}", reservationCode);

            try
            {
                var reservation = await _reservationService.GetReservationByCodeAsync(reservationCode);

                if (reservation == null)
                {
                    _logger.LogWarning("Rezervimi me kod {Code} nuk u gjet", reservationCode);
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
                _logger.LogError(ex, "Gabim gjatë shfaqjes së faqes së suksesit");
                return RedirectToAction("Index", "Home");
            }
        }

        /// <summary>
        /// GET: /Reservation/Failed
        /// Faqja e dështimit
        /// </summary>
        [HttpGet]
        public IActionResult Failed()
        {
            return View();
        }
    }
}
