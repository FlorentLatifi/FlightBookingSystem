using System.Collections.Generic;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Repositories
{
    public interface IBookingRepository
    {
        Task<List<Booking>> GetAllAsync();
        Task<Booking?> GetByIdAsync(int id);
        Task<Booking?> GetByReferenceAsync(string bookingReference);
        Task AddAsync(Booking booking);
        Task UpdateAsync(Booking booking);
        Task DeleteAsync(int id);
    }
}

