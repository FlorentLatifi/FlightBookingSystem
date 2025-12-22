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
    /// Implementimi i IReservationRepository
    /// </summary>
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;

        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passenger)
                .Include(r => r.Payment)
                .OrderByDescending(r => r.ReservationDate)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passenger)
                .Include(r => r.Payment)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reservation?> GetByReservationCodeAsync(string reservationCode)
        {
            return await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Passenger)
                .Include(r => r.Payment)
                .FirstOrDefaultAsync(r => r.ReservationCode == reservationCode);
        }

        public async Task<IEnumerable<Reservation>> GetByPassengerIdAsync(int passengerId)
        {
            return await _context.Reservations
                .Include(r => r.Flight)
                .Include(r => r.Payment)
                .Where(r => r.PassengerId == passengerId)
                .OrderByDescending(r => r.ReservationDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<Reservation>> GetByFlightIdAsync(int flightId)
        {
            return await _context.Reservations
                .Include(r => r.Passenger)
                .Include(r => r.Payment)
                .Where(r => r.FlightId == flightId)
                .OrderBy(r => r.SeatNumber)
                .ToListAsync();
        }

        public async Task AddAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var reservation = await GetByIdAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }
    }
}
