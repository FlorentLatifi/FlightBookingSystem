using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Queries
{
    /// <summary>
    /// Query për të marrë detajet e një fluturimi sipas ID
    /// CQRS Pattern - Separate Queries from Commands
    /// </summary>
    public class GetFlightQuery
    {
        public int FlightId { get; set; }

        public GetFlightQuery(int flightId)
        {
            FlightId = flightId;
        }
    }

    /// <summary>
    /// Handler për GetFlightQuery
    /// </summary>
    public class GetFlightQueryHandler
    {
        private readonly IFlightRepository _flightRepository;

        public GetFlightQueryHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<Flight?> Handle(GetFlightQuery query)
        {
            return await _flightRepository.GetByIdAsync(query.FlightId);
        }
    }
}