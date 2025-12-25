using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// Interface për logjikën e biznesit të fluturimeve
    /// </summary>
    public interface IFlightService
    {
        Task<List<Flight>> SearchAvailableFlightsAsync(string origin, string destination, DateTime departureDate);
        Task<Flight?> GetFlightDetailsAsync(int flightId);
        Task<bool> CheckAvailabilityAsync(int flightId, int numberOfSeats);
        Task ReserveSeatsAsync(int flightId, int numberOfSeats);
        Task ReleaseSeatsAsync(int flightId, int numberOfSeats);
    }
}
