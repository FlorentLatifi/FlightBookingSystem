using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DbSets
        // =========================
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Route> Routes { get; set; }

        // =========================
        // Model Configuration
        // =========================
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply all IEntityTypeConfiguration<T>
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(ApplicationDbContext).Assembly
            );

            // =========================
            // Seed Data - CORRECTED
            // =========================
            SeedAirports(modelBuilder);
            SeedAirlines(modelBuilder);
            SeedFlights(modelBuilder);
            SeedSeats(modelBuilder);
        }

        private void SeedAirports(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>().HasData(
                new Airport { Id = 1, Code = "PRN", Name = "Prishtina International Airport", City = "Prishtina", Country = "Kosovo", Timezone = "Europe/Belgrade" },
                new Airport { Id = 2, Code = "VIE", Name = "Vienna International Airport", City = "Vienna", Country = "Austria", Timezone = "Europe/Vienna" },
                new Airport { Id = 3, Code = "MUC", Name = "Munich Airport", City = "Munich", Country = "Germany", Timezone = "Europe/Berlin" },
                new Airport { Id = 4, Code = "FCO", Name = "Leonardo da Vinci–Fiumicino Airport", City = "Rome", Country = "Italy", Timezone = "Europe/Rome" },
                new Airport { Id = 5, Code = "LHR", Name = "London Heathrow Airport", City = "London", Country = "United Kingdom", Timezone = "Europe/London" },
                new Airport { Id = 6, Code = "CDG", Name = "Charles de Gaulle Airport", City = "Paris", Country = "France", Timezone = "Europe/Paris" },
                new Airport { Id = 7, Code = "TIA", Name = "Tirana International Airport", City = "Tirana", Country = "Albania", Timezone = "Europe/Tirane" }
            );
        }

        private void SeedAirlines(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>().HasData(
                new Airline { Id = 1, Code = "W6", Name = "Wizz Air", Country = "Hungary", LogoUrl = "" },
                new Airline { Id = 2, Code = "LH", Name = "Lufthansa", Country = "Germany", LogoUrl = "" },
                new Airline { Id = 3, Code = "OS", Name = "Austrian Airlines", Country = "Austria", LogoUrl = "" },
                new Airline { Id = 4, Code = "FR", Name = "Ryanair", Country = "Ireland", LogoUrl = "" },
                new Airline { Id = 5, Code = "BA", Name = "British Airways", Country = "United Kingdom", LogoUrl = "" }
            );
        }

        private void SeedFlights(ModelBuilder modelBuilder)
        {
            var now = DateTime.Now;
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    FlightNumber = "W6-3301",
                    AirlineId = 1,
                    DepartureAirportId = 1,
                    ArrivalAirportId = 2,
                    Airline = "Wizz Air",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "VIE - Vienna",
                    Origin = "",
                    Destination = "",
                    DepartureTime = now.AddDays(5).Date.AddHours(6),
                    ArrivalTime = now.AddDays(5).Date.AddHours(8),
                    BasePriceAmount = 49.99m,
                    BasePriceCurrency = "USD",
                    TotalSeats = 180,
                    AvailableSeats = 180,
                    Status = FlightStatus.Scheduled
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "LH-1428",
                    AirlineId = 2,
                    DepartureAirportId = 1,
                    ArrivalAirportId = 3,
                    Airline = "Lufthansa",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "MUC - Munich",
                    Origin = "",
                    Destination = "",
                    DepartureTime = now.AddDays(5).Date.AddHours(10),
                    ArrivalTime = now.AddDays(5).Date.AddHours(12),
                    BasePriceAmount = 89.99m,
                    BasePriceCurrency = "USD",
                    TotalSeats = 150,
                    AvailableSeats = 150,
                    Status = FlightStatus.Scheduled
                },
                new Flight
                {
                    Id = 3,
                    FlightNumber = "OS-730",
                    AirlineId = 3,
                    DepartureAirportId = 1,
                    ArrivalAirportId = 2,
                    Airline = "Austrian Airlines",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "VIE - Vienna",
                    Origin = "",
                    Destination = "",
                    DepartureTime = now.AddDays(9).Date.AddHours(14),
                    ArrivalTime = now.AddDays(9).Date.AddHours(16),
                    BasePriceAmount = 79.99m,
                    BasePriceCurrency = "USD",
                    TotalSeats = 120,
                    AvailableSeats = 120,
                    Status = FlightStatus.Scheduled
                },
                new Flight
                {
                    Id = 4,
                    FlightNumber = "FR-9152",
                    AirlineId = 4,
                    DepartureAirportId = 7,
                    ArrivalAirportId = 4,
                    Airline = "Ryanair",
                    DepartureAirport = "TIA - Tirana",
                    ArrivalAirport = "FCO - Rome",
                    Origin = "",
                    Destination = "",
                    DepartureTime = now.AddDays(4).Date.AddHours(8),
                    ArrivalTime = now.AddDays(4).Date.AddHours(10),
                    BasePriceAmount = 39.99m,
                    BasePriceCurrency = "USD",
                    TotalSeats = 189,
                    AvailableSeats = 189,
                    Status = FlightStatus.Scheduled
                },
                new Flight
                {
                    Id = 5,
                    FlightNumber = "BA-2612",
                    AirlineId = 5,
                    DepartureAirportId = 1,
                    ArrivalAirportId = 5,
                    Airline = "British Airways",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "LHR - London Heathrow",
                    Origin = "",
                    Destination = "",
                    DepartureTime = now.AddDays(13).Date.AddHours(7),
                    ArrivalTime = now.AddDays(13).Date.AddHours(10).AddMinutes(30),
                    BasePriceAmount = 149.99m,
                    BasePriceCurrency = "USD",
                    TotalSeats = 200,
                    AvailableSeats = 200,
                    Status = FlightStatus.Scheduled
                }
            );
        }

        private void SeedSeats(ModelBuilder modelBuilder)
        {
            // Seed minimal seats for testing
            var seats = new List<Seat>();
            int seatId = 1;

            // For each flight, create some sample seats
            for (int flightId = 1; flightId <= 5; flightId++)
            {
                // Economy seats (rows 25-30)
                for (int row = 25; row <= 30; row++)
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

                // Business seats (rows 5-10)
                for (int row = 5; row <= 10; row++)
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
            }

            modelBuilder.Entity<Seat>().HasData(seats);
        }
    }
}