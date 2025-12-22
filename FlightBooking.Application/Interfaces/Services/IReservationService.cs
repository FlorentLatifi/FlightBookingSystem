using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// Interface për logjikën e biznesit të rezervimeve
    /// </summary>
    public interface IReservationService
    {
        /// <summary>
        /// Krijon një rezervim të ri
        /// </summary>
        Task<Reservation> CreateReservationAsync(
            int flightId,
            int passengerId,
            SeatClass seatClass);

        /// <summary>
        /// Merr detajet e rezervimit sipas kodit
        /// </summary>
        Task<Reservation?> GetReservationByCodeAsync(string reservationCode);

        /// <summary>
        /// Konfirmon një rezervim (pasi pagesa është bërë)
        /// </summary>
        Task ConfirmReservationAsync(int reservationId);

        /// <summary>
        /// Anulon një rezervim
        /// </summary>
        Task CancelReservationAsync(int reservationId);

        /// <summary>
        /// Merr të gjitha rezervimet e një pasagjeri
        /// </summary>
        Task<IEnumerable<Reservation>> GetPassengerReservationsAsync(int passengerId);

        /// <summary>
        /// Gjeneron një kod unik rezervimi (p.sh. RES-ABC123)
        /// </summary>
        string GenerateReservationCode();
    }
}
