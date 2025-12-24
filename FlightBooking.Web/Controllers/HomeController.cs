using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FlightBooking.Web.Models;

using FlightBooking.Application.DTOs;
using FlightBooking.Application.Interfaces.Services;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// Controller për faqen kryesore
    /// Shfaq search form për fluturime
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IFlightService _flightService;
        private readonly ILogger<HomeController> _logger;

        public HomeController(
            IFlightService flightService,
            ILogger<HomeController> logger)
        {
            _flightService = flightService;
            _logger = logger;
        }

        /// <summary>
        /// GET: /Home/Index
        /// Faqja kryesore me search form
        /// </summary>
        public IActionResult Index()
        {
            _logger.LogInformation("Faqja kryesore u aksesua");

            // Krijo një DTO të zbrazët për search form
            var searchDto = new FlightSearchDto
            {
                DepartureDate = DateTime.Now.AddDays(7) // Default: 7 ditë nga sot
            };

            return View(searchDto);
        }

        /// <summary>
        /// POST: /Home/Search
        /// Kërkon fluturime dhe redirecton te rezultatet
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(FlightSearchDto searchDto)
        {
            _logger.LogInformation(
                "Kërkesë për fluturime: {From} -> {To} në {Date}",
                searchDto.DepartureAirport,
                searchDto.ArrivalAirport,
                searchDto.DepartureDate);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Të dhënat e search form nuk janë të vlefshme");
                return View("Index", searchDto);
            }

            try
            {
                // Kërko fluturime
                var flights = await _flightService.SearchAvailableFlightsAsync(
                    searchDto.DepartureAirport,
                    searchDto.ArrivalAirport,
                    searchDto.DepartureDate);

                var flightDtos = flights.Select(f => new FlightDto
                {
                    Id = f.Id,
                    FlightNumber = f.FlightNumber,
                    Airline = f.Airline,
                    DepartureAirport = f.DepartureAirport,
                    ArrivalAirport = f.ArrivalAirport,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    BasePrice = f.BasePrice,
                    AvailableSeats = f.AvailableSeats,
                    Status = f.Status,
                    DurationMinutes = f.DurationMinutes
                }).ToList();

                _logger.LogInformation("U gjetën {Count} fluturime", flightDtos.Count);

                // Ruaj search criteria në TempData për ta shfaqur në rezultate
                TempData["SearchCriteria"] = $"{searchDto.DepartureAirport} → {searchDto.ArrivalAirport} | {searchDto.DepartureDate:dd/MM/yyyy}";

                // Kaloje listën te Flights/Results
                return View("SearchResults", flightDtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Gabim gjatë kërkimit të fluturimeve");
                ModelState.AddModelError("", "Ndodhi një gabim gjatë kërkimit. Ju lutem provoni përsëri.");
                return View("Index", searchDto);
            }
        }

        /// <summary>
        /// GET: /Home/About
        /// Faqja About
        /// </summary>
        public IActionResult About()
        {
            return View();
        }

        /// <summary>
        /// GET: /Home/Error
        /// Faqja e gabimit
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}