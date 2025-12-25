using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using FlightBooking.Application.Exceptions;
using FlightBooking.Domain.Entities;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;

namespace FlightBooking.Application.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
        }

        public async Task<List<Flight>> SearchAvailableFlightsAsync(string origin, string destination, DateTime departureDate)
        {
            var flights = await _flightRepository.GetAllAsync();

            return flights
                .Where(f => (f.Origin == origin || f.DepartureAirport.Contains(origin))
                    && (f.Destination == destination || f.ArrivalAirport.Contains(destination))
                    && f.DepartureTime.Date == departureDate.Date
                    && f.AvailableSeats > 0)
                .OrderBy(f => f.DepartureTime)
                .ToList();
        }

        public async Task<Flight?> GetFlightDetailsAsync(int flightId)
        {
            return await _flightRepository.GetByIdAsync(flightId);
        }

        public async Task<bool> CheckAvailabilityAsync(int flightId, int numberOfSeats)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight == null)
                return false;

            return flight.CanBook(numberOfSeats);
        }

        public async Task ReserveSeatsAsync(int flightId, int numberOfSeats)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight == null)
                throw new NotFoundException($"Flight with ID {flightId} not found");

            flight.ReserveSeat(numberOfSeats);
            await _flightRepository.UpdateAsync(flight);
        }

        public async Task ReleaseSeatsAsync(int flightId, int numberOfSeats)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight == null)
                throw new NotFoundException($"Flight with ID {flightId} not found");

            flight.ReleaseSeat(numberOfSeats);
            await _flightRepository.UpdateAsync(flight);
        }
    }
}