using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// Interface për logjikën e biznesit të fluturimeve
    /// </summary>
    public interface IFlightService
    {
        /// <summary>
        /// Kërkon fluturime të disponueshme
        /// </summary>
        Task<IEnumerable<Flight>> SearchAvailableFlightsAsync(
            string departureAirport,
            string arrivalAirport,
            DateTime departureDate);

        /// <summary>
        /// Merr detajet e një fluturimi
        /// </summary>
        Task<Flight?> GetFlightDetailsAsync(int flightId);

        /// <summary>
        /// Kontrollon nëse fluturimi ka vende të disponueshme
        /// </summary>
        Task<bool> CheckAvailabilityAsync(int flightId, int numberOfSeats);

        /// <summary>
        /// Rezervon vende në fluturim (pakëson AvailableSeats)
        /// </summary>
        Task ReserveSeatsAsync(int flightId, int numberOfSeats);

        /// <summary>
        /// Çliron vende në fluturim (shtoin AvailableSeats)
        /// Përdoret kur anulohet rezervimi
        /// </summary>
        Task ReleaseSeatsAsync(int flightId, int numberOfSeats);
    }
}
