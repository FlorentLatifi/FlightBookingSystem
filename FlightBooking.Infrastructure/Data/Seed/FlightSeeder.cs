using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeder për Flight data
    /// Krijon fluturime për testim (30 ditë në të ardhmen)
    /// </summary>
    public static class FlightSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var now = DateTime.Now;
            var flights = new List<Flight>();
            int flightId = 1;

            // Krijo fluturime për 30 ditët e ardhshme
            for (int dayOffset = 1; dayOffset <= 30; dayOffset++)
            {
                var departureDate = now.AddDays(dayOffset).Date;

                // PRN -> VIE (Wizz Air) - Çdo ditë në 06:00
                flights.Add(CreateFlight(
                    id: flightId++,
                    flightNumber: $"W6-{3300 + dayOffset}",
                    airlineId: 1,
                    airline: "Wizz Air",
                    depAirportId: 1,
                    depAirport: "PRN - Prishtina",
                    arrAirportId: 2,
                    arrAirport: "VIE - Vienna",
                    departureTime: departureDate.AddHours(6),
                    arrivalTime: departureDate.AddHours(8),
                    basePrice: 49.99m,
                    totalSeats: 180
                ));

                // PRN -> VIE (Austrian Airlines) - Çdo ditë në 14:00
                flights.Add(CreateFlight(
                    id: flightId++,
                    flightNumber: $"OS-{730 + dayOffset}",
                    airlineId: 3,
                    airline: "Austrian Airlines",
                    depAirportId: 1,
                    depAirport: "PRN - Prishtina",
                    arrAirportId: 2,
                    arrAirport: "VIE - Vienna",
                    departureTime: departureDate.AddHours(14),
                    arrivalTime: departureDate.AddHours(16),
                    basePrice: 79.99m,
                    totalSeats: 120
                ));

                // PRN -> MUC (Lufthansa) - Çdo ditë në 10:00
                flights.Add(CreateFlight(
                    id: flightId++,
                    flightNumber: $"LH-{1400 + dayOffset}",
                    airlineId: 2,
                    airline: "Lufthansa",
                    depAirportId: 1,
                    depAirport: "PRN - Prishtina",
                    arrAirportId: 3,
                    arrAirport: "MUC - Munich",
                    departureTime: departureDate.AddHours(10),
                    arrivalTime: departureDate.AddHours(12),
                    basePrice: 89.99m,
                    totalSeats: 150
                ));

                // Fluturime shtesë për ditë specifike
                if (dayOffset % 2 == 0) // Çdo 2 ditë
                {
                    // PRN -> LHR (British Airways)
                    flights.Add(CreateFlight(
                        id: flightId++,
                        flightNumber: $"BA-{2600 + dayOffset}",
                        airlineId: 5,
                        airline: "British Airways",
                        depAirportId: 1,
                        depAirport: "PRN - Prishtina",
                        arrAirportId: 5,
                        arrAirport: "LHR - London Heathrow",
                        departureTime: departureDate.AddHours(7),
                        arrivalTime: departureDate.AddHours(10).AddMinutes(30),
                        basePrice: 149.99m,
                        totalSeats: 200
                    ));
                }

                if (dayOffset % 3 == 0) // Çdo 3 ditë
                {
                    // TIA -> FCO (Ryanair)
                    flights.Add(CreateFlight(
                        id: flightId++,
                        flightNumber: $"FR-{9100 + dayOffset}",
                        airlineId: 4,
                        airline: "Ryanair",
                        depAirportId: 7,
                        depAirport: "TIA - Tirana",
                        arrAirportId: 4,
                        arrAirport: "FCO - Rome",
                        departureTime: departureDate.AddHours(8),
                        arrivalTime: departureDate.AddHours(10),
                        basePrice: 39.99m,
                        totalSeats: 189
                    ));
                }

                if (dayOffset % 4 == 0) // Çdo 4 ditë
                {
                    // PRN -> CDG (Air France)
                    flights.Add(CreateFlight(
                        id: flightId++,
                        flightNumber: $"AF-{1100 + dayOffset}",
                        airlineId: 6,
                        airline: "Air France",
                        depAirportId: 1,
                        depAirport: "PRN - Prishtina",
                        arrAirportId: 6,
                        arrAirport: "CDG - Paris",
                        departureTime: departureDate.AddHours(9),
                        arrivalTime: departureDate.AddHours(11).AddMinutes(30),
                        basePrice: 99.99m,
                        totalSeats: 180
                    ));
                }
            }

            modelBuilder.Entity<Flight>().HasData(flights);
        }

        private static Flight CreateFlight(
            int id,
            string flightNumber,
            int airlineId,
            string airline,
            int depAirportId,
            string depAirport,
            int arrAirportId,
            string arrAirport,
            DateTime departureTime,
            DateTime arrivalTime,
            decimal basePrice,
            int totalSeats)
        {
            return new Flight
            {
                Id = id,
                FlightNumber = flightNumber,
                AirlineId = airlineId,
                Airline = airline,
                DepartureAirportId = depAirportId,
                DepartureAirport = depAirport,
                ArrivalAirportId = arrAirportId,
                ArrivalAirport = arrAirport,
                Origin = "", // Legacy field
                Destination = "", // Legacy field
                DepartureTime = departureTime,
                ArrivalTime = arrivalTime,
                BasePriceAmount = basePrice,
                BasePriceCurrency = "USD",
                TotalSeats = totalSeats,
                AvailableSeats = totalSeats,
                Status = FlightStatus.Scheduled
            };
        }
    }
}