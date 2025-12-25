using System.Collections.Generic;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Repositories
{
    public interface IFlightRepository
    {
        Task<IEnumerable<Flight>> GetAllAsync();
        Task<Flight?> GetByIdAsync(int id);
        Task AddAsync(Flight flight);
        Task UpdateAsync(Flight flight);
        Task DeleteAsync(int id);
    }
}