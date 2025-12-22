using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Repositories
{
    /// <summary>
    /// Interface për aksesin në të dhënat e pagesave
    /// </summary>
    public interface IPaymentRepository
    {
        /// <summary>
        /// Merr të gjitha pagesat
        /// </summary>
        Task<IEnumerable<Payment>> GetAllAsync();

        /// <summary>
        /// Merr një pagesë sipas ID
        /// </summary>
        Task<Payment?> GetByIdAsync(int id);

        /// <summary>
        /// Merr pagesën për një rezervim specifik
        /// </summary>
        Task<Payment?> GetByReservationIdAsync(int reservationId);

        /// <summary>
        /// Merr pagesën sipas Transaction ID
        /// </summary>
        Task<Payment?> GetByTransactionIdAsync(string transactionId);

        /// <summary>
        /// Shton një pagesë të re
        /// </summary>
        Task AddAsync(Payment payment);

        /// <summary>
        /// Përditëson një pagesë ekzistuese
        /// </summary>
        Task UpdateAsync(Payment payment);

        /// <summary>
        /// Fshin një pagesë
        /// </summary>
        Task DeleteAsync(int id);
    }
}
