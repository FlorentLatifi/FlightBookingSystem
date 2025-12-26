using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Queries
{
    /// <summary>
    /// Query për të kërkuar fluturime
    /// CQRS Pattern - Complex Read Operation
    /// </summary>
    public class SearchFlightsQuery
    {
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public int? NumberOfPassengers { get; set; }
        public SeatClass? PreferredClass { get; set; }
        public decimal? MaxPrice { get; set; }

        public SearchFlightsQuery()
        {
        }

        public SearchFlightsQuery(
            string departureAirport,
            string arrivalAirport,
            DateTime departureDate,
            int? numberOfPassengers = null)
        {
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            DepartureDate = departureDate;
            NumberOfPassengers = numberOfPassengers;
        }
    }

    /// <summary>
    /// Handler për SearchFlightsQuery
    /// Implementon logjikën e kërkimit me filtra
    /// </summary>
    public class SearchFlightsQueryHandler
    {
        private readonly IFlightRepository _flightRepository;

        public SearchFlightsQueryHandler(IFlightRepository flightRepository)
        {
            _flightRepository = flightRepository;
        }

        public async Task<List<Flight>> Handle(SearchFlightsQuery query)
        {
            // Validime
            if (string.IsNullOrWhiteSpace(query.DepartureAirport))
                throw new ArgumentException("Departure airport is required", nameof(query.DepartureAirport));

            if (string.IsNullOrWhiteSpace(query.ArrivalAirport))
                throw new ArgumentException("Arrival airport is required", nameof(query.ArrivalAirport));

            if (query.DepartureDate.Date < DateTime.Now.Date)
                throw new ArgumentException("Departure date cannot be in the past", nameof(query.DepartureDate));

            // Merr të gjitha fluturimet
            var allFlights = await _flightRepository.GetAllAsync();

            // Filtro fluturimet
            var filteredFlights = allFlights
                .Where(f =>
                    // Filtro sipas aeroportit të nisjes
                    (f.DepartureAirport.Contains(query.DepartureAirport, StringComparison.OrdinalIgnoreCase) ||
                     f.Origin.Contains(query.DepartureAirport, StringComparison.OrdinalIgnoreCase)) &&

                    // Filtro sipas aeroportit të mbërritjes
                    (f.ArrivalAirport.Contains(query.ArrivalAirport, StringComparison.OrdinalIgnoreCase) ||
                     f.Destination.Contains(query.ArrivalAirport, StringComparison.OrdinalIgnoreCase)) &&

                    // Filtro sipas datës
                    f.DepartureTime.Date == query.DepartureDate.Date &&

                    // Vetëm fluturime të planifikuara
                    f.Status == FlightStatus.Scheduled &&

                    // Kontrollo disponueshmërinë
                    f.AvailableSeats > 0)
                .ToList();

            // Filtro sipas numrit të pasagjerëve (nëse specifikuar)
            if (query.NumberOfPassengers.HasValue && query.NumberOfPassengers.Value > 0)
            {
                filteredFlights = filteredFlights
                    .Where(f => f.AvailableSeats >= query.NumberOfPassengers.Value)
                    .ToList();
            }

            // Filtro sipas çmimit maksimal (nëse specifikuar)
            if (query.MaxPrice.HasValue)
            {
                filteredFlights = filteredFlights
                    .Where(f => f.BasePriceAmount <= query.MaxPrice.Value)
                    .ToList();
            }

            // Rendit sipas kohës së nisjes
            return filteredFlights
                .OrderBy(f => f.DepartureTime)
                .ToList();
        }
    }
}