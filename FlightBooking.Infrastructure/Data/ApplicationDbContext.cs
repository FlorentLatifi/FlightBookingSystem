using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Data
{
    /// <summary>
    /// DbContext për aplikacionin
    /// Menaxhon komunikimin me bazën e të dhënave
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // DbSets - Tabelat në bazë të dhënash
        public DbSet<Flight> Flights => Set<Flight>();
        public DbSet<Passenger> Passengers => Set<Passenger>();
        public DbSet<Reservation> Reservations => Set<Reservation>();
        public DbSet<Payment> Payments => Set<Payment>();

        /// <summary>
        /// Konfigurimi i marrëdhënieve dhe constraint-ave
        /// </summary>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // =============================================
            // FLIGHT CONFIGURATION
            // =============================================
            modelBuilder.Entity<Flight>(entity =>
            {
                entity.HasKey(f => f.Id);

                entity.Property(f => f.FlightNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(f => f.Airline)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(f => f.DepartureAirport)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(f => f.ArrivalAirport)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(f => f.BasePrice)
                    .HasColumnType("decimal(18,2)");

                // Index për kërkime më të shpejta
                entity.HasIndex(f => f.FlightNumber);
                entity.HasIndex(f => new { f.DepartureAirport, f.ArrivalAirport, f.DepartureTime });

                // Relationship: Flight → Reservations (One-to-Many)
                entity.HasMany(f => f.Reservations)
                    .WithOne(r => r.Flight)
                    .HasForeignKey(r => r.FlightId)
                    .OnDelete(DeleteBehavior.Restrict); // Nuk mund të fshihet flight me rezervime
            });

            // =============================================
            // PASSENGER CONFIGURATION
            // =============================================
            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.Email)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.PassportNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(p => p.Nationality)
                    .IsRequired()
                    .HasMaxLength(50);

                // Index për email dhe passport number (duhet të jenë unikë)
                entity.HasIndex(p => p.Email).IsUnique();
                entity.HasIndex(p => p.PassportNumber).IsUnique();

                // Relationship: Passenger → Reservations (One-to-Many)
                entity.HasMany(p => p.Reservations)
                    .WithOne(r => r.Passenger)
                    .HasForeignKey(r => r.PassengerId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // =============================================
            // RESERVATION CONFIGURATION
            // =============================================
            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(r => r.Id);

                entity.Property(r => r.ReservationCode)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(r => r.SeatNumber)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(r => r.TotalPrice)
                    .HasColumnType("decimal(18,2)");

                // Index për reservation code (duhet të jetë unik)
                entity.HasIndex(r => r.ReservationCode).IsUnique();

                // Relationship: Reservation → Payment (One-to-One)
                entity.HasOne(r => r.Payment)
                    .WithOne(p => p.Reservation)
                    .HasForeignKey<Payment>(p => p.ReservationId)
                    .OnDelete(DeleteBehavior.Cascade); // Nëse fshihet reservation, fshihet edhe payment
            });

            // =============================================
            // PAYMENT CONFIGURATION
            // =============================================
            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(p => p.Amount)
                    .HasColumnType("decimal(18,2)");

                entity.Property(p => p.PaymentMethod)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(p => p.TransactionId)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(p => p.PaymentGatewayResponse)
                    .HasMaxLength(500);

                // Index për transaction ID (duhet të jetë unik)
                entity.HasIndex(p => p.TransactionId).IsUnique();
            });

            // =============================================
            // SEED DATA (Të dhëna fillestare për testing)
            // =============================================
            SeedData(modelBuilder);
        }

        /// <summary>
        /// Shton të dhëna fillestare në bazë për testing
        /// </summary>
        private void SeedData(ModelBuilder modelBuilder)
        {
            // Seed Flights
            modelBuilder.Entity<Flight>().HasData(
                new Flight
                {
                    Id = 1,
                    FlightNumber = "W6-3301",
                    Airline = "Wizz Air",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "VIE - Vienna",
                    DepartureTime = DateTime.Now.AddDays(7).Date.AddHours(6),
                    ArrivalTime = DateTime.Now.AddDays(7).Date.AddHours(8),
                    BasePrice = 49.99m,
                    TotalSeats = 180,
                    AvailableSeats = 180,
                    Status = FlightStatus.Scheduled,
                    DurationMinutes = 120
                },
                new Flight
                {
                    Id = 2,
                    FlightNumber = "LH-1428",
                    Airline = "Lufthansa",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "MUC - Munich",
                    DepartureTime = DateTime.Now.AddDays(7).Date.AddHours(10),
                    ArrivalTime = DateTime.Now.AddDays(7).Date.AddHours(12),
                    BasePrice = 89.99m,
                    TotalSeats = 150,
                    AvailableSeats = 150,
                    Status = FlightStatus.Scheduled,
                    DurationMinutes = 120
                },
                new Flight
                {
                    Id = 3,
                    FlightNumber = "OS-730",
                    Airline = "Austrian Airlines",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "VIE - Vienna",
                    DepartureTime = DateTime.Now.AddDays(10).Date.AddHours(14),
                    ArrivalTime = DateTime.Now.AddDays(10).Date.AddHours(16),
                    BasePrice = 79.99m,
                    TotalSeats = 120,
                    AvailableSeats = 120,
                    Status = FlightStatus.Scheduled,
                    DurationMinutes = 120
                },
                new Flight
                {
                    Id = 4,
                    FlightNumber = "FR-9152",
                    Airline = "Ryanair",
                    DepartureAirport = "TIA - Tirana",
                    ArrivalAirport = "FCO - Rome",
                    DepartureTime = DateTime.Now.AddDays(5).Date.AddHours(8),
                    ArrivalTime = DateTime.Now.AddDays(5).Date.AddHours(10),
                    BasePrice = 39.99m,
                    TotalSeats = 189,
                    AvailableSeats = 189,
                    Status = FlightStatus.Scheduled,
                    DurationMinutes = 120
                },
                new Flight
                {
                    Id = 5,
                    FlightNumber = "BA-2612",
                    Airline = "British Airways",
                    DepartureAirport = "PRN - Prishtina",
                    ArrivalAirport = "LHR - London Heathrow",
                    DepartureTime = DateTime.Now.AddDays(14).Date.AddHours(7),
                    ArrivalTime = DateTime.Now.AddDays(14).Date.AddHours(10).AddMinutes(30),
                    BasePrice = 149.99m,
                    TotalSeats = 200,
                    AvailableSeats = 200,
                    Status = FlightStatus.Scheduled,
                    DurationMinutes = 210
                }
            );
        }
    }
}
