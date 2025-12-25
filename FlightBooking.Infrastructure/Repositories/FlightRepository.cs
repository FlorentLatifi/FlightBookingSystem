using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using FlightBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Repositories
{
    /// <summary>
    /// Implementimi i IFlightRepository
    /// Komunikon me bazën e të dhënave për operacione me Flights
    /// </summary>
    public class FlightRepository : IFlightRepository, FlightBooking.Application.Interfaces.Repositories.IFlightRepository
    {
        private readonly ApplicationDbContext _context;

        public FlightRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Flight>> GetAllAsync()
        {
            return await _context.Flights
                .Include(f => f.Reservations)
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<Flight?> GetByIdAsync(int id)
        {
            return await _context.Flights
                .Include(f => f.Reservations)
                    .ThenInclude(r => r.Passenger)
                .Include(f => f.Reservations)
                    .ThenInclude(r => r.Payment)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<IEnumerable<Flight>> SearchFlightsAsync(
            string departureAirport,
            string arrivalAirport,
            DateTime departureDate)
        {
            // Kërko fluturime që:
            // 1. Nisin nga aeroporti i kërkuar
            // 2. Shkojnë në aeroportin e kërkuar
            // 3. Nisin në datën e kërkuar (pa orë specifike)
            var startOfDay = departureDate.Date;
            var endOfDay = startOfDay.AddDays(1);

            return await _context.Flights
                .Where(f =>
                    f.DepartureAirport.Contains(departureAirport) &&
                    f.ArrivalAirport.Contains(arrivalAirport) &&
                    f.DepartureTime >= startOfDay &&
                    f.DepartureTime < endOfDay)
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task<IEnumerable<Flight>> GetAvailableFlightsAsync()
        {
            return await _context.Flights
                .Where(f =>
                    f.Status == FlightStatus.Scheduled &&
                    f.AvailableSeats > 0 &&
                    f.DepartureTime > DateTime.Now)
                .OrderBy(f => f.DepartureTime)
                .ToListAsync();
        }

        public async Task AddAsync(Flight flight)
        {
            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Flight flight)
        {
            _context.Flights.Update(flight);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var flight = await GetByIdAsync(id);
            if (flight != null)
            {
                _context.Flights.Remove(flight);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Flight?> GetByFlightNumberAsync(string flightNumber)
        {
            return await _context.Flights
                .FirstOrDefaultAsync(f => f.FlightNumber == flightNumber);
        }
    }
}
