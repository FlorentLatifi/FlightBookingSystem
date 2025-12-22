using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Repositories
{
    /// <summary>
    /// Implementimi i IPassengerRepository
    /// </summary>
    public class PassengerRepository : IPassengerRepository
    {
        private readonly ApplicationDbContext _context;

        public PassengerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Passenger>> GetAllAsync()
        {
            return await _context.Passengers
                .Include(p => p.Reservations)
                .OrderBy(p => p.LastName)
                .ThenBy(p => p.FirstName)
                .ToListAsync();
        }

        public async Task<Passenger?> GetByIdAsync(int id)
        {
            return await _context.Passengers
                .Include(p => p.Reservations)
                    .ThenInclude(r => r.Flight)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Passenger?> GetByEmailAsync(string email)
        {
            return await _context.Passengers
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task<Passenger?> GetByPassportNumberAsync(string passportNumber)
        {
            return await _context.Passengers
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.PassportNumber == passportNumber);
        }

        public async Task AddAsync(Passenger passenger)
        {
            await _context.Passengers.AddAsync(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Passenger passenger)
        {
            _context.Passengers.Update(passenger);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var passenger = await GetByIdAsync(id);
            if (passenger != null)
            {
                _context.Passengers.Remove(passenger);
                await _context.SaveChangesAsync();
            }
        }
    }
}