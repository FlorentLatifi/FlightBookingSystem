using Microsoft.AspNetCore.Mvc;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Strategies.Pricing;
using FlightBooking.Application.DTOs;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Web.Controllers
{
    /// <summary>
    /// DESIGN PATTERN: Strategy Pattern - API Controller
    /// API endpoint për llogaritjen e çmimeve me strategji të ndryshme
    /// Përdoret nga UI për të demonstruar efektin e strategjive
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PricingApiController : ControllerBase
    {
        private readonly IFlightService _flightService;
        private readonly IPricingStrategy _pricingStrategy;

        public PricingApiController(
            IFlightService flightService,
            IPricingStrategy pricingStrategy)
        {
            _flightService = flightService;
            _pricingStrategy = pricingStrategy;
        }

        /// <summary>
        /// Llogarit çmimin për një fluturim me strategji të caktuar
        /// </summary>
        [HttpGet("calculate")]
        public async Task<IActionResult> CalculatePrice(
            [FromQuery] int flightId,
            [FromQuery] SeatClass seatClass = SeatClass.Economy,
            [FromQuery] int numberOfSeats = 1,
            [FromQuery] string? strategy = null)
        {
            var flight = await _flightService.GetFlightDetailsAsync(flightId);
            if (flight == null)
                return NotFound(new { error = "Flight not found" });

            // Nëse specifikohet strategji, përdor atë (për demo)
            // Në realitet, kjo do të ndryshonte strategjinë në DI
            var price = _pricingStrategy.CalculatePrice(flight, seatClass, numberOfSeats);

            return Ok(new
            {
                flightId = flight.Id,
                flightNumber = flight.FlightNumber,
                seatClass = seatClass.ToString(),
                numberOfSeats = numberOfSeats,
                basePrice = flight.BasePriceAmount,
                calculatedPrice = price.Amount,
                currency = price.Currency,
                strategy = _pricingStrategy.StrategyName,
                description = _pricingStrategy.GetDescription()
            });
        }

        /// <summary>
        /// Krahason çmimet me të gjitha strategjitë e disponueshme
        /// </summary>
        [HttpGet("compare")]
        public async Task<IActionResult> ComparePrices(
            [FromQuery] int flightId,
            [FromQuery] SeatClass seatClass = SeatClass.Economy,
            [FromQuery] int numberOfSeats = 1)
        {
            var flight = await _flightService.GetFlightDetailsAsync(flightId);
            if (flight == null)
                return NotFound(new { error = "Flight not found" });

            // Për momentin kthejmë vetëm strategjinë aktuale
            // Në realitet, kjo do të kthente të gjitha strategjitë
            var price = _pricingStrategy.CalculatePrice(flight, seatClass, numberOfSeats);

            return Ok(new
            {
                flightId = flight.Id,
                flightNumber = flight.FlightNumber,
                seatClass = seatClass.ToString(),
                numberOfSeats = numberOfSeats,
                basePrice = flight.BasePriceAmount,
                strategies = new[]
                {
                    new
                    {
                        name = _pricingStrategy.StrategyName,
                        price = price.Amount,
                        description = _pricingStrategy.GetDescription()
                    }
                }
            });
        }
    }
}

