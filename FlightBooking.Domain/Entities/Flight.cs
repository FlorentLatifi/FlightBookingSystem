using System;
using System.Collections.Generic;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një fluturim
    /// DESIGN PATTERN: Domain-Driven Design
    /// Përmban business rules dhe validime
    /// </summary>
    public class Flight
    {
        // =========================
        // Primary Key
        // =========================
        public int Id { get; set; }

        // =========================
        // Flight Information
        // =========================
        public string FlightNumber { get; set; } = string.Empty;

        // =========================
        // Foreign Keys - NEW ARCHITECTURE
        // =========================
        public int DepartureAirportId { get; set; }
        public int ArrivalAirportId { get; set; }
        public int AirlineId { get; set; }

        // =========================
        // Legacy Fields (për backward compatibility)
        // Gradualisht do të hiqen kur të përditësohen të gjitha views
        // =========================
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;

        // =========================
        // Flight Times
        // =========================
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }

        // =========================
        // Pricing
        // =========================
        public decimal BasePriceAmount { get; set; }
        public string BasePriceCurrency { get; set; } = "USD";

        // =========================
        // Capacity
        // =========================
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }

        // =========================
        // Status
        // =========================
        public FlightStatus Status { get; set; }

        // =========================
        // Navigation Properties
        // =========================
        public virtual Airport? DepartureAirportEntity { get; set; }
        public virtual Airport? ArrivalAirportEntity { get; set; }
        public virtual Airline? AirlineEntity { get; set; }
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // =========================
        // Computed Properties
        // =========================
        [NotMapped]
        public Money BasePrice
        {
            get => new Money(BasePriceAmount, BasePriceCurrency);
            set
            {
                BasePriceAmount = value.Amount;
                BasePriceCurrency = value.Currency;
            }
        }

        [NotMapped]
        public int DurationMinutes => (int)(ArrivalTime - DepartureTime).TotalMinutes;

        // =========================
        // Constructors
        // =========================

        /// <summary>
        /// Parameterless constructor for EF Core
        /// </summary>
        public Flight()
        {
        }

        /// <summary>
        /// Constructor for business logic
        /// </summary>
        public Flight(
            string flightNumber,
            string origin,
            string destination,
            DateTime departureTime,
            DateTime arrivalTime,
            Money basePrice,
            int totalSeats)
        {
            FlightNumber = flightNumber ?? throw new ArgumentNullException(nameof(flightNumber));
            Origin = origin ?? throw new ArgumentNullException(nameof(origin));
            Destination = destination ?? throw new ArgumentNullException(nameof(destination));

            if (departureTime >= arrivalTime)
                throw new ArgumentException("Departure must be before arrival");

            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            BasePrice = basePrice ?? throw new ArgumentNullException(nameof(basePrice));
            TotalSeats = totalSeats;
            AvailableSeats = totalSeats;
            Status = FlightStatus.Scheduled;
        }

        // =========================
        // Business Logic
        // =========================

        /// <summary>
        /// A mund të rezervohet ky fluturim për numrin e dhënë të ulëseve?
        /// </summary>
        public bool CanBook(int numberOfSeats)
        {
            return Status == FlightStatus.Scheduled
                && AvailableSeats >= numberOfSeats
                && DepartureTime > DateTime.UtcNow.AddHours(2);
        }

        /// <summary>
        /// A mund të rezervohet ky fluturim?
        /// </summary>
        public bool CanBeBooked()
        {
            return CanBook(1);
        }

        /// <summary>
        /// Rezervon ulëse në fluturim
        /// </summary>
        public void ReserveSeat(int count)
        {
            if (!CanBook(count))
                throw new InvalidOperationException("Cannot reserve seats for this flight");

            AvailableSeats -= count;
        }

        /// <summary>
        /// Çliron ulëse (kur anulohet rezervimi)
        /// </summary>
        public void ReleaseSeat(int count)
        {
            AvailableSeats += count;

            if (AvailableSeats > TotalSeats)
                AvailableSeats = TotalSeats;
        }

        /// <summary>
        /// Anulon fluturimin
        /// </summary>
        public void CancelFlight()
        {
            Status = FlightStatus.Cancelled;
        }

        /// <summary>
        /// Merr kohëzgjatjen e fluturimit
        /// </summary>
        public TimeSpan GetDuration()
        {
            return ArrivalTime - DepartureTime;
        }

        /// <summary>
        /// A është fluturim ndërkombëtar?
        /// </summary>
        public bool IsInternational()
        {
            return Origin != Destination && GetDuration() > TimeSpan.FromHours(3);
        }
    }
}