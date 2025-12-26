using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Queries
{
    /// <summary>
    /// Query për të marrë një rezervim sipas kodit
    /// CQRS Pattern - Read Operation
    /// </summary>
    public class GetReservationByCodeQuery
    {
        public string ReservationCode { get; set; }

        public GetReservationByCodeQuery(string reservationCode)
        {
            ReservationCode = reservationCode;
        }
    }

    /// <summary>
    /// Handler për GetReservationByCodeQuery
    /// </summary>
    public class GetReservationByCodeQueryHandler
    {
        private readonly IReservationRepository _reservationRepository;

        public GetReservationByCodeQueryHandler(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<Reservation?> Handle(GetReservationByCodeQuery query)
        {
            if (string.IsNullOrWhiteSpace(query.ReservationCode))
            {
                throw new ArgumentException("Reservation code cannot be empty", nameof(query.ReservationCode));
            }

            return await _reservationRepository.GetByReservationCodeAsync(query.ReservationCode);
        }
    }
}
