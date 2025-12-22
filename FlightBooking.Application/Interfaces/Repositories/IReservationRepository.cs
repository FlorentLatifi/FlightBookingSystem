using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface për aksesin në të dhënat e rezervimeve
    /// </summary>
    public interface IReservationRepository
    {
        /// <summary>
        /// Merr të gjitha rezervimet
        /// </summary>
        Task<IEnumerable<Reservation>> GetAllAsync();

        /// <summary>
        /// Merr një rezervim sipas ID
        /// </summary>
        Task<Reservation?> GetByIdAsync(int id);

        /// <summary>
        /// Merr një rezervim sipas kodit të rezervimit
        /// </summary>
        Task<Reservation?> GetByReservationCodeAsync(string reservationCode);

        /// <summary>
        /// Merr të gjitha rezervimet e një pasagjeri
        /// </summary>
        Task<IEnumerable<Reservation>> GetByPassengerIdAsync(int passengerId);

        /// <summary>
        /// Merr të gjitha rezervimet e një fluturimi
        /// </summary>
        Task<IEnumerable<Reservation>> GetByFlightIdAsync(int flightId);

        /// <summary>
        /// Shton një rezervim të ri
        /// </summary>
        Task AddAsync(Reservation reservation);

        /// <summary>
        /// Përditëson një rezervim ekzistues
        /// </summary>
        Task UpdateAsync(Reservation reservation);

        /// <summary>
        /// Fshin një rezervim
        /// </summary>
        Task DeleteAsync(int id);
    }
}
