using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Repositories
{
    public class SeatRepository : FlightBooking.Application.Interfaces.Repositories.ISeatRepository
    {
        private readonly ApplicationDbContext _context;

        public SeatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Seat>> GetByFlightIdAsync(int flightId)
        {
            return await _context.Set<Seat>()
                .Where(s => s.FlightId == flightId)
                .ToListAsync();
        }

        public async Task<Seat?> GetBySeatNumberAsync(int flightId, string seatNumber)
        {
            return await _context.Set<Seat>()
                .FirstOrDefaultAsync(s => s.FlightId == flightId && s.SeatNumberValue == seatNumber);
        }

        public async Task<Seat?> GetByIdAsync(int id)
        {
            return await _context.Set<Seat>()
                .Include(s => s.Flight)
                .Include(s => s.Booking)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task AddAsync(Seat seat)
        {
            await _context.Set<Seat>().AddAsync(seat);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Seat seat)
        {
            _context.Set<Seat>().Update(seat);
            await _context.SaveChangesAsync();
        }
    }
}

