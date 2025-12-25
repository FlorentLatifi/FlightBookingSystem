using System;
using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Infrastructure.Data
{
    /// <summary>
    /// DESIGN PATTERN: Repository Pattern - DbContext
    /// ApplicationDbContext është qendra e data access layer
    /// Përmban të gjitha DbSets dhe konfigurimin e relationships
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Core Entities
        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Payment> Payments => Set<Payment>();
        public DbSet<Booking> Bookings => Set<Booking>();
        public DbSet<Seat> Seats => Set<Seat>();

        // Reference Data Entities
        public DbSet<Airport> Airports => Set<Airport>();
        public DbSet<Airline> Airlines => Set<Airline>();
        public DbSet<Route> Routes => Set<Route>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            ConfigureAirport(modelBuilder);
            ConfigureAirline(modelBuilder);
            ConfigureRoute(modelBuilder);
            ConfigureUser(modelBuilder);
            ConfigureFlight(modelBuilder);
            ConfigurePassenger(modelBuilder);
            ConfigureReservation(modelBuilder);
            ConfigurePayment(modelBuilder);
            ConfigureSeat(modelBuilder);
            ConfigureBooking(modelBuilder);

            // Seed Data
            SeedData(modelBuilder);
        }

        private void ConfigureAirport(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airport>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Code).IsRequired().HasMaxLength(10);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(200);
                entity.Property(a => a.City).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Country).IsRequired().HasMaxLength(100);
                entity.Property(a => a.Timezone).HasMaxLength(50);

                entity.HasIndex(a => a.Code).IsUnique();
            });
        }

        private void ConfigureAirline(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>(entity =>
            {
                entity.HasKey(a => a.Id);
                entity.Property(a => a.Code).IsRequired().HasMaxLength(10);
                entity.Property(a => a.Name).IsRequired().HasMaxLength(200);
                entity.Property(a => a.Country).HasMaxLength(100);
                entity.Property(a => a.LogoUrl).HasMaxLength(500);

                entity.HasIndex(a => a.Code).IsUnique();
            });
        }

        private void ConfigureRoute(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Route>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.DistanceKm).HasColumnType("decimal(18,2)");

                entity.HasOne(r => r.DepartureAirport)
                      .WithMany()
                      .HasForeignKey(r => r.DepartureAirportId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.ArrivalAirport)
                      .WithMany()
                      .HasForeignKey(r => r.ArrivalAirportId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasIndex(r => new { r.DepartureAirportId, r.ArrivalAirportId }).IsUnique();
            });
        }

        private void ConfigureUser(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username).IsRequired().HasMaxLength(50);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.PasswordHash).IsRequired().HasMaxLength(500);
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Role).IsRequired().HasMaxLength(50);

                entity.HasIndex(u => u.Username).IsUnique();
                entity.HasIndex(u => u.Email).IsUnique();
            });
        }

        private void ConfigureFlight(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.Property(f => f.FlightNumber).IsRequired().HasMaxLength(10);
                entity.Property(f => f.BasePriceAmount).HasColumnType("decimal(18,2)");
                entity.Property(f => f.BasePriceCurrency).IsRequired().HasMaxLength(3);

                // Legacy string properties (për backward compatibility)
                entity.Property(f => f.Airline).HasMaxLength(100);
                entity.Property(f => f.DepartureAirport).HasMaxLength(100);
                entity.Property(f => f.ArrivalAirport).HasMaxLength(100);
                entity.Property(f => f.Origin).HasMaxLength(100);
                entity.Property(f => f.Destination).HasMaxLength(100);

                entity.HasIndex(f => f.FlightNumber);
                entity.HasIndex(f => new { f.DepartureAirportId, f.ArrivalAirportId, f.DepartureTime });

                // Foreign Keys
                entity.HasOne(f => f.DepartureAirportEntity)
                      .WithMany(a => a.DepartureFlights)
                      .HasForeignKey(f => f.DepartureAirportId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.ArrivalAirportEntity)
                      .WithMany(a => a.ArrivalFlights)
                      .HasForeignKey(f => f.ArrivalAirportId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.AirlineEntity)
                      .WithMany(a => a.Flights)
                      .HasForeignKey(f => f.AirlineId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Relationships
                entity.HasMany(f => f.Reservations)
                      .WithOne(r => r.Flight)
                      .HasForeignKey(r => r.FlightId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(f => f.Bookings)
                      .WithOne(b => b.Flight)
                      .HasForeignKey(b => b.FlightId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(f => f.Seats)
                      .WithOne(s => s.Flight)
                      .HasForeignKey(s => s.FlightId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurePassenger(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FirstName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.LastName).IsRequired().HasMaxLength(50);
                entity.Property(p => p.Email).IsRequired().HasMaxLength(100);
                entity.Property(p => p.PhoneNumber).IsRequired().HasMaxLength(20);
                entity.Property(p => p.PassportNumber).IsRequired().HasMaxLength(20);
                entity.Property(p => p.Nationality).IsRequired().HasMaxLength(50);

                entity.HasIndex(p => p.Email).IsUnique();
                entity.HasIndex(p => p.PassportNumber).IsUnique();

                entity.HasMany(p => p.Reservations)
                      .WithOne(r => r.Passenger)
                      .HasForeignKey(r => r.PassengerId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureReservation(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.ReservationCode).IsRequired().HasMaxLength(20);
                entity.Property(r => r.SeatNumber).IsRequired().HasMaxLength(10);
                entity.Property(r => r.TotalPrice).HasColumnType("decimal(18,2)");

                entity.HasIndex(r => r.ReservationCode).IsUnique();

                entity.HasOne(r => r.Payment)
                      .WithOne(p => p.Reservation)
                      .HasForeignKey<Payment>(p => p.ReservationId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigurePayment(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Amount).HasColumnType("decimal(18,2)");
                entity.Property(p => p.Currency).IsRequired().HasMaxLength(3);
                entity.Property(p => p.PaymentMethod).IsRequired().HasMaxLength(50);
                entity.Property(p => p.TransactionId).IsRequired().HasMaxLength(100);
                entity.Property(p => p.PaymentGatewayResponse).HasMaxLength(500);

                entity.HasIndex(p => p.TransactionId).IsUnique();
            });
        }

        private void ConfigureSeat(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seat>(entity =>
            {
                entity.HasKey(s => s.Id);

                entity.Property(s => s.SeatNumberValue).IsRequired();
                entity.Property(s => s.IsAvailable).IsRequired();
                entity.Property(s => s.Class).IsRequired();

                entity.HasOne(s => s.Booking)
                      .WithMany(b => b.Seats)
                      .HasForeignKey(s => s.BookingId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(s => s.Flight)
                      .WithMany(f => f.Seats)
                      .HasForeignKey(s => s.FlightId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        private void ConfigureBooking(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.HasKey(b => b.Id);

                entity.Property(b => b.BookingReference).IsRequired().HasMaxLength(20);
                entity.Property(b => b.PassengerName).IsRequired().HasMaxLength(100);
                entity.Property(b => b.PassengerEmail).IsRequired().HasMaxLength(100);
                entity.Property(b => b.PassengerPhone).IsRequired().HasMaxLength(20);
                entity.Property(b => b.TotalPriceAmount).HasColumnType("decimal(18,2)");
                entity.Property(b => b.TotalPriceCurrency).IsRequired().HasMaxLength(3);

                entity.HasIndex(b => b.BookingReference).IsUnique();

                entity.HasOne(b => b.Flight)
                      .WithMany(f => f.Bookings)
                      .HasForeignKey(b => b.FlightId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(b => b.Payment)
                      .WithOne(p => p.Booking)
                      .HasForeignKey<Payment>(p => p.BookingId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

        /// <summary>
        /// Seed data: 10 airports, 8 airlines, 50+ flights, 3+ passengers, 3+ reservations
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            var now = DateTime.UtcNow;

            // ================= AIRPORTS (10) =================
            var airports = new[]
            {
                new Airport { Id = 1, Code = "PRN", Name = "Prishtina International Airport", City = "Prishtina", Country = "Kosovo", Timezone = "Europe/Belgrade" },
                new Airport { Id = 2, Code = "VIE", Name = "Vienna International Airport", City = "Vienna", Country = "Austria", Timezone = "Europe/Vienna" },
                new Airport { Id = 3, Code = "MUC", Name = "Munich Airport", City = "Munich", Country = "Germany", Timezone = "Europe/Berlin" },
                new Airport { Id = 4, Code = "FCO", Name = "Leonardo da Vinci–Fiumicino Airport", City = "Rome", Country = "Italy", Timezone = "Europe/Rome" },
                new Airport { Id = 5, Code = "LHR", Name = "London Heathrow Airport", City = "London", Country = "United Kingdom", Timezone = "Europe/London" },
                new Airport { Id = 6, Code = "CDG", Name = "Charles de Gaulle Airport", City = "Paris", Country = "France", Timezone = "Europe/Paris" },
                new Airport { Id = 7, Code = "TIA", Name = "Tirana International Airport", City = "Tirana", Country = "Albania", Timezone = "Europe/Tirane" },
                new Airport { Id = 8, Code = "IST", Name = "Istanbul Airport", City = "Istanbul", Country = "Turkey", Timezone = "Europe/Istanbul" },
                new Airport { Id = 9, Code = "ATH", Name = "Athens International Airport", City = "Athens", Country = "Greece", Timezone = "Europe/Athens" },
                new Airport { Id = 10, Code = "ZRH", Name = "Zurich Airport", City = "Zurich", Country = "Switzerland", Timezone = "Europe/Zurich" }
            };
            modelBuilder.Entity<Airport>().HasData(airports);

            // ================= AIRLINES (8) =================
            var airlines = new[]
            {
                new Airline { Id = 1, Code = "W6", Name = "Wizz Air", Country = "Hungary", LogoUrl = "" },
                new Airline { Id = 2, Code = "LH", Name = "Lufthansa", Country = "Germany", LogoUrl = "" },
                new Airline { Id = 3, Code = "OS", Name = "Austrian Airlines", Country = "Austria", LogoUrl = "" },
                new Airline { Id = 4, Code = "FR", Name = "Ryanair", Country = "Ireland", LogoUrl = "" },
                new Airline { Id = 5, Code = "BA", Name = "British Airways", Country = "United Kingdom", LogoUrl = "" },
                new Airline { Id = 6, Code = "AF", Name = "Air France", Country = "France", LogoUrl = "" },
                new Airline { Id = 7, Code = "TK", Name = "Turkish Airlines", Country = "Turkey", LogoUrl = "" },
                new Airline { Id = 8, Code = "LX", Name = "Swiss International Air Lines", Country = "Switzerland", LogoUrl = "" }
            };
            modelBuilder.Entity<Airline>().HasData(airlines);

            // ================= FLIGHTS (50+) =================
            var flights = new List<Flight>();
            var flightId = 1;
            var random = new Random(42); // Fixed seed për konsistencë

            // Generate flights për çdo kombinim të airports dhe airlines
            for (int dayOffset = 1; dayOffset <= 30; dayOffset++)
            {
                var departureDate = now.AddDays(dayOffset).Date;

                // PRN → VIE (Wizz Air, Austrian Airlines)
                flights.Add(CreateFlight(flightId++, "W6", 1, 1, 2, departureDate.AddHours(6), departureDate.AddHours(8), 49.99m, 180, 1));
                flights.Add(CreateFlight(flightId++, "OS", 3, 1, 2, departureDate.AddHours(14), departureDate.AddHours(16), 79.99m, 120, 3));

                // PRN → MUC (Lufthansa)
                flights.Add(CreateFlight(flightId++, "LH", 2, 1, 3, departureDate.AddHours(10), departureDate.AddHours(12), 89.99m, 150, 2));

                // PRN → LHR (British Airways)
                if (dayOffset % 2 == 0)
                {
                    flights.Add(CreateFlight(flightId++, "BA", 5, 1, 5, departureDate.AddHours(7), departureDate.AddHours(10).AddMinutes(30), 149.99m, 200, 5));
                }

                // TIA → FCO (Ryanair)
                if (dayOffset % 3 == 0)
                {
                    flights.Add(CreateFlight(flightId++, "FR", 4, 7, 4, departureDate.AddHours(8), departureDate.AddHours(10), 39.99m, 189, 4));
                }

                // VIE → CDG (Air France)
                if (dayOffset % 4 == 0)
                {
                    flights.Add(CreateFlight(flightId++, "AF", 6, 2, 6, departureDate.AddHours(9), departureDate.AddHours(11), 99.99m, 180, 6));
                }
            }

            modelBuilder.Entity<Flight>().HasData(flights);

            // ================= PASSENGERS (3+) =================
            var passengers = new[]
            {
                new Passenger
                {
                    Id = 1,
                    FirstName = "Florent",
                    LastName = "Latifi",
                    Email = "florent.latifi@example.com",
                    PhoneNumber = "+38349123456",
                    PassportNumber = "KS123456",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    Nationality = "Kosovar"
                },
                new Passenger
                {
                    Id = 2,
                    FirstName = "Arben",
                    LastName = "Krasniqi",
                    Email = "arben.krasniqi@example.com",
                    PhoneNumber = "+38349234567",
                    PassportNumber = "KS234567",
                    DateOfBirth = new DateTime(1992, 8, 20),
                    Nationality = "Kosovar"
                },
                new Passenger
                {
                    Id = 3,
                    FirstName = "Blerta",
                    LastName = "Berisha",
                    Email = "blerta.berisha@example.com",
                    PhoneNumber = "+38349345678",
                    PassportNumber = "KS345678",
                    DateOfBirth = new DateTime(1998, 3, 10),
                    Nationality = "Kosovar"
                },
                new Passenger
                {
                    Id = 4,
                    FirstName = "Dardan",
                    LastName = "Gashi",
                    Email = "dardan.gashi@example.com",
                    PhoneNumber = "+38349456789",
                    PassportNumber = "KS456789",
                    DateOfBirth = new DateTime(1990, 11, 25),
                    Nationality = "Kosovar"
                }
            };
            modelBuilder.Entity<Passenger>().HasData(passengers);

            // ================= RESERVATIONS (3+) =================
            var reservations = new[]
            {
                new Reservation
                {
                    Id = 1,
                    ReservationCode = "RES-ABC123",
                    FlightId = 1,
                    PassengerId = 1,
                    SeatClass = SeatClass.Economy,
                    SeatNumber = "12A",
                    TotalPrice = 49.99m,
                    Status = ReservationStatus.Confirmed,
                    ReservationDate = now.AddDays(-5)
                },
                new Reservation
                {
                    Id = 2,
                    ReservationCode = "RES-DEF456",
                    FlightId = 2,
                    PassengerId = 2,
                    SeatClass = SeatClass.Business,
                    SeatNumber = "5C",
                    TotalPrice = 224.98m, // 89.99 * 2.5
                    Status = ReservationStatus.Pending,
                    ReservationDate = now.AddDays(-3)
                },
                new Reservation
                {
                    Id = 3,
                    ReservationCode = "RES-GHI789",
                    FlightId = 3,
                    PassengerId = 3,
                    SeatClass = SeatClass.PremiumEconomy,
                    SeatNumber = "15B",
                    TotalPrice = 119.99m, // 79.99 * 1.5
                    Status = ReservationStatus.Confirmed,
                    ReservationDate = now.AddDays(-2)
                }
            };
            modelBuilder.Entity<Reservation>().HasData(reservations);
        }

        private Flight CreateFlight(int id, string airlineCode, int airlineId, int departureAirportId, int arrivalAirportId, 
            DateTime departureTime, DateTime arrivalTime, decimal basePrice, int totalSeats, int? originalId = null)
        {
            // Map airline codes to names
            var airlineNames = new Dictionary<string, string>
            {
                { "W6", "Wizz Air" }, { "LH", "Lufthansa" }, { "OS", "Austrian Airlines" },
                { "FR", "Ryanair" }, { "BA", "British Airways" }, { "AF", "Air France" },
                { "TK", "Turkish Airlines" }, { "LX", "Swiss International Air Lines" }
            };

            // Map airport IDs to codes and cities
            var airportData = new Dictionary<int, (string Code, string City)>
            {
                { 1, ("PRN", "Prishtina") }, { 2, ("VIE", "Vienna") }, { 3, ("MUC", "Munich") },
                { 4, ("FCO", "Rome") }, { 5, ("LHR", "London") }, { 6, ("CDG", "Paris") },
                { 7, ("TIA", "Tirana") }, { 8, ("IST", "Istanbul") }, { 9, ("ATH", "Athens") },
                { 10, ("ZRH", "Zurich") }
            };

            var airline = airlineNames.GetValueOrDefault(airlineCode, airlineCode);
            var depAirport = airportData.GetValueOrDefault(departureAirportId, ("", ""));
            var arrAirport = airportData.GetValueOrDefault(arrivalAirportId, ("", ""));

            return new Flight
            {
                Id = id,
                FlightNumber = $"{airlineCode}-{id:D4}",
                AirlineId = airlineId,
                DepartureAirportId = departureAirportId,
                ArrivalAirportId = arrivalAirportId,
                Airline = airline,
                DepartureAirport = $"{depAirport.Code} - {depAirport.City}",
                ArrivalAirport = $"{arrAirport.Code} - {arrAirport.City}",
                Origin = depAirport.City,
                Destination = arrAirport.City,
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
