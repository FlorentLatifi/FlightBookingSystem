using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Data.Seed;

namespace FlightBooking.Infrastructure.Data
{
    /// <summary>
    /// Database Context për Flight Booking System
    /// DESIGN PATTERN: Repository Pattern
    /// Përdor Entity Framework Core për data access
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // =========================
        // DbSets - Entity Collections
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

            // Apply all IEntityTypeConfiguration<T> from assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // =========================
            // Seed Data - Clean & Professional
            // =========================
            AirportSeeder.Seed(modelBuilder);
            AirlineSeeder.Seed(modelBuilder);
            FlightSeeder.Seed(modelBuilder);
            SeatSeeder.Seed(modelBuilder);
        }
    }
}