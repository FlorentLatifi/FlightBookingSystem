using System.Collections.Generic;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Repositories
{
    public interface ISeatRepository
    {
        Task<List<Seat>> GetByFlightIdAsync(int flightId);
        Task<Seat?> GetBySeatNumberAsync(int flightId, string seatNumber);
        Task<Seat?> GetByIdAsync(int id);
        Task AddAsync(Seat seat);
        Task UpdateAsync(Seat seat);
    }
}

