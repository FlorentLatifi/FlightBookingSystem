using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Infrastructure.Data.Seed
{
    public static class FlightSeeder
    {
        public static Flight[] GetFlights()
        {
            var now = DateTime.UtcNow;
            var flights = new List<Flight>();
            var flightId = 1;

            for (int dayOffset = 1; dayOffset <= 30; dayOffset++)
            {
                var departureDate = now.AddDays(dayOffset).Date;

                flights.Add(CreateFlight(flightId++, "W6", 1, 1, 2, departureDate.AddHours(6), departureDate.AddHours(8), 49.99m, 180));
                flights.Add(CreateFlight(flightId++, "OS", 3, 1, 2, departureDate.AddHours(14), departureDate.AddHours(16), 79.99m, 120));
                flights.Add(CreateFlight(flightId++, "LH", 2, 1, 3, departureDate.AddHours(10), departureDate.AddHours(12), 89.99m, 150));

                if (dayOffset % 2 == 0)
                {
                    flights.Add(CreateFlight(flightId++, "BA", 5, 1, 5, departureDate.AddHours(7), departureDate.AddHours(10).AddMinutes(30), 149.99m, 200));
                }

                if (dayOffset % 3 == 0)
                {
                    flights.Add(CreateFlight(flightId++, "FR", 4, 7, 4, departureDate.AddHours(8), departureDate.AddHours(10), 39.99m, 189));
                }

                if (dayOffset % 4 == 0)
                {
                    flights.Add(CreateFlight(flightId++, "AF", 6, 2, 6, departureDate.AddHours(9), departureDate.AddHours(11), 99.99m, 180));
                }
            }

            return flights.ToArray();
        }

        private static Flight CreateFlight(
            int id,
            string airlineCode,
            int airlineId,
            int departureAirportId,
            int arrivalAirportId,
            DateTime departureTime,
            DateTime arrivalTime,
            decimal basePrice,
            int totalSeats)
        {
            return new Flight
            {
                Id = id,
                FlightNumber = $"{airlineCode}-{id:D4}",
                AirlineId = airlineId,
                DepartureAirportId = departureAirportId,
                ArrivalAirportId = arrivalAirportId,
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

