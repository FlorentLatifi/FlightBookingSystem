using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface për aksesin në të dhënat e pasagjerëve
    /// </summary>
    public interface IPassengerRepository
    {
        /// <summary>
        /// Merr të gjithë pasagjerët
        /// </summary>
        Task<IEnumerable<Passenger>> GetAllAsync();

        /// <summary>
        /// Merr një pasagjer sipas ID
        /// </summary>
        Task<Passenger?> GetByIdAsync(int id);

        /// <summary>
        /// Merr një pasagjer sipas email
        /// </summary>
        Task<Passenger?> GetByEmailAsync(string email);

        /// <summary>
        /// Merr një pasagjer sipas numrit të pasaportës
        /// </summary>
        Task<Passenger?> GetByPassportNumberAsync(string passportNumber);

        /// <summary>
        /// Shton një pasagjer të ri
        /// </summary>
        Task AddAsync(Passenger passenger);

        /// <summary>
        /// Përditëson një pasagjer ekzistues
        /// </summary>
        Task UpdateAsync(Passenger passenger);

        /// <summary>
        /// Fshin një pasagjer
        /// </summary>
        Task DeleteAsync(int id);
    }
}
