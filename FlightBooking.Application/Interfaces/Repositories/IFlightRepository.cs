using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface për aksesin në të dhënat e fluturimeve
    /// Përcakton operacionet CRUD dhe kërkime
    /// </summary>
    public interface IFlightRepository
    {
        /// <summary>
        /// Merr të gjithë fluturimet
        /// </summary>
        Task<IEnumerable<Flight>> GetAllAsync();

        /// <summary>
        /// Merr një fluturim sipas ID
        /// </summary>
        Task<Flight?> GetByIdAsync(int id);

        /// <summary>
        /// Kërkon fluturime sipas aeroportit të nisjes dhe destinacionit
        /// </summary>
        Task<IEnumerable<Flight>> SearchFlightsAsync(
            string departureAirport,
            string arrivalAirport,
            DateTime departureDate);

        /// <summary>
        /// Merr fluturimet e disponueshme (që mund të rezervohen)
        /// </summary>
        Task<IEnumerable<Flight>> GetAvailableFlightsAsync();

        /// <summary>
        /// Shton një fluturim të ri
        /// </summary>
        Task AddAsync(Flight flight);

        /// <summary>
        /// Përditëson një fluturim ekzistues
        /// </summary>
        Task UpdateAsync(Flight flight);

        /// <summary>
        /// Fshin një fluturim
        /// </summary>
        Task DeleteAsync(int id);

        /// <summary>
        /// Merr fluturimet sipas numrit të fluturimit
        /// </summary>
        Task<Flight?> GetByFlightNumberAsync(string flightNumber);
    }
}

