using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Repositories
{
    public class BookingRepository : FlightBooking.Application.Interfaces.Repositories.IBookingRepository
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Booking>> GetAllAsync()
        {
            return await _context.Set<Booking>()
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .Include(b => b.Payment)
                .ToListAsync();
        }

        public async Task<Booking?> GetByIdAsync(int id)
        {
            return await _context.Set<Booking>()
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<Booking?> GetByReferenceAsync(string bookingReference)
        {
            return await _context.Set<Booking>()
                .Include(b => b.Flight)
                .Include(b => b.Seats)
                .Include(b => b.Payment)
                .FirstOrDefaultAsync(b => b.BookingReference == bookingReference);
        }

        public async Task AddAsync(Booking booking)
        {
            await _context.Set<Booking>().AddAsync(booking);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Booking booking)
        {
            _context.Set<Booking>().Update(booking);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var booking = await GetByIdAsync(id);
            if (booking != null)
            {
                _context.Set<Booking>().Remove(booking);
                await _context.SaveChangesAsync();
            }
        }
    }
}

