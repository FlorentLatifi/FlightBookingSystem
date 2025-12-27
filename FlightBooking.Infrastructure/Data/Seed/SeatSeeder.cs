using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeder për Seat data
    /// Krijon ulëse për çdo fluturim
    /// </summary>
    public static class SeatSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            var seats = new List<Seat>();
            int seatId = 1;

            // Për thjeshtësi, krijo ulëse vetëm për 5 fluturimet e para
            // Në migration të plotë, mund të krijohen për të gjitha
            for (int flightId = 1; flightId <= 5; flightId++)
            {
                // First Class (rows 1-4, seats A-D)
                for (int row = 1; row <= 4; row++)
                {
                    foreach (var letter in new[] { "A", "B", "C", "D" })
                    {
                        seats.Add(new Seat
                        {
                            Id = seatId++,
                            FlightId = flightId,
                            SeatNumberValue = $"{row}{letter}",
                            Class = SeatClass.FirstClass,
                            IsAvailable = true
                        });
                    }
                }

                // Business Class (rows 5-14, seats A-D)
                for (int row = 5; row <= 14; row++)
                {
                    foreach (var letter in new[] { "A", "B", "C", "D" })
                    {
                        seats.Add(new Seat
                        {
                            Id = seatId++,
                            FlightId = flightId,
                            SeatNumberValue = $"{row}{letter}",
                            Class = SeatClass.Business,
                            IsAvailable = true
                        });
                    }
                }

                // Premium Economy (rows 15-24, seats A-F)
                for (int row = 15; row <= 24; row++)
                {
                    foreach (var letter in new[] { "A", "B", "C", "D", "E", "F" })
                    {
                        seats.Add(new Seat
                        {
                            Id = seatId++,
                            FlightId = flightId,
                            SeatNumberValue = $"{row}{letter}",
                            Class = SeatClass.PremiumEconomy,
                            IsAvailable = true
                        });
                    }
                }

                // Economy (rows 25-40, seats A-F)
                for (int row = 25; row <= 40; row++)
                {
                    foreach (var letter in new[] { "A", "B", "C", "D", "E", "F" })
                    {
                        seats.Add(new Seat
                        {
                            Id = seatId++,
                            FlightId = flightId,
                            SeatNumberValue = $"{row}{letter}",
                            Class = SeatClass.Economy,
                            IsAvailable = true
                        });
                    }
                }
            }

            modelBuilder.Entity<Seat>().HasData(seats);
        }
    }
}
