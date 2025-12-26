using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.DTOs;
using FlightBooking.Application.Validators;

namespace FlightBooking.Web.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        // GET: /Flight/Search
        [HttpGet]
        public IActionResult Search()
        {
            return View(new FlightSearchDto());
        }

        // POST: /Flight/Search
        [HttpPost]
        public async Task<IActionResult> Search(FlightSearchDto dto)
        {
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

            var flights = await _flightService.SearchAvailableFlightsAsync(
                dto.DepartureAirport,
                dto.ArrivalAirport,
                dto.DepartureDate
            );

            return View("SearchResults", flights);
        }

        // GET: /Flight/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var flight = await _flightService.GetFlightDetailsAsync(id);

            if (flight == null)
                return NotFound();

            return View(flight);
        }
    }
}
