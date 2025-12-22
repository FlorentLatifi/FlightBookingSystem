using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Services
{
    /// <summary>
    /// Service për logjikën e biznesit të fluturimeve
    /// </summary>
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;

        public FlightService(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        /// <summary>
        /// Kërkon fluturime të disponueshme
        /// </summary>
        public async Task<IEnumerable<Flight>> SearchAvailableFlightsAsync(
            string departureAirport,
            string arrivalAirport,
            DateTime departureDate)
        {
            // Validime
            if (string.IsNullOrWhiteSpace(departureAirport))
                throw new ArgumentException("Aeroporti i nisjes është i detyrueshëm", nameof(departureAirport));

            if (string.IsNullOrWhiteSpace(arrivalAirport))
                throw new ArgumentException("Aeroporti i mbërritjes është i detyrueshëm", nameof(arrivalAirport));

            if (departureDate.Date < DateTime.Now.Date)
                throw new ArgumentException("Data e nisjes nuk mund të jetë në të kaluarën", nameof(departureDate));

            // Kërko fluturime
            var flights = await _flightRepository.SearchFlightsAsync(
                departureAirport,
                arrivalAirport,
                departureDate);

            // Filtro vetëm fluturimet që mund të rezervohen
            return flights.Where(f => f.CanBeBooked()).ToList();
        }

        /// <summary>
        /// Merr detajet e një fluturimi
        /// </summary>
        public async Task<Flight?> GetFlightDetailsAsync(int flightId)
        {
            if (flightId <= 0)
                throw new ArgumentException("ID e fluturimit nuk është e vlefshme", nameof(flightId));

            return await _flightRepository.GetByIdAsync(flightId);
        }

        /// <summary>
        /// Kontrollon nëse fluturimi ka vende të disponueshme
        /// </summary>
        public async Task<bool> CheckAvailabilityAsync(int flightId, int numberOfSeats)
        {
            if (flightId <= 0)
                throw new ArgumentException("ID e fluturimit nuk është e vlefshme", nameof(flightId));

            if (numberOfSeats <= 0)
                throw new ArgumentException("Numri i ulëseve duhet të jetë më i madh se 0", nameof(numberOfSeats));

            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight == null)
                return false;

            return flight.AvailableSeats >= numberOfSeats && flight.CanBeBooked();
        }

        /// <summary>
        /// Rezervon vende në fluturim (pakëson AvailableSeats)
        /// </summary>
        public async Task ReserveSeatsAsync(int flightId, int numberOfSeats)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight == null)
                throw new InvalidOperationException($"Fluturimi me ID {flightId} nuk u gjet");

            if (!flight.CanBeBooked())
                throw new InvalidOperationException("Ky fluturim nuk mund të rezervohet");

            if (flight.AvailableSeats < numberOfSeats)
                throw new InvalidOperationException($"Vetëm {flight.AvailableSeats} ulëse janë të disponueshme");

            // Pakëso vendet e disponueshme
            flight.AvailableSeats -= numberOfSeats;

            await _flightRepository.UpdateAsync(flight);

            Console.WriteLine($"[FlightService] {numberOfSeats} ulëse u rezervuan për fluturimin {flight.FlightNumber}. " +
                            $"Vende të mbetura: {flight.AvailableSeats}");
        }

        /// <summary>
        /// Çliron vende në fluturim (shtoin AvailableSeats)
        /// Përdoret kur anulohet rezervimi
        /// </summary>
        public async Task ReleaseSeatsAsync(int flightId, int numberOfSeats)
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);

            if (flight == null)
                throw new InvalidOperationException($"Fluturimi me ID {flightId} nuk u gjet");

            // Shtoni vendet e disponueshme
            flight.AvailableSeats += numberOfSeats;

            // Sigurohuni që nuk kalon TotalSeats
            if (flight.AvailableSeats > flight.TotalSeats)
                flight.AvailableSeats = flight.TotalSeats;

            await _flightRepository.UpdateAsync(flight);

            Console.WriteLine($"[FlightService] {numberOfSeats} ulëse u çliruan për fluturimin {flight.FlightNumber}. " +
                            $"Vende të disponueshme: {flight.AvailableSeats}");
        }
    }
}
