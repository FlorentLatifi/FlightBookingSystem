using System;
using System.Collections.Generic;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    public class Flight
    {
        public int Id { get; set; } // Public setter for EF Core
        public string FlightNumber { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public string Airline { get; set; } = string.Empty;
        public string DepartureAirport { get; set; } = string.Empty;
        public string ArrivalAirport { get; set; } = string.Empty;
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePriceAmount { get; set; }
        public string BasePriceCurrency { get; set; } = "USD";
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public FlightStatus Status { get; set; }

        // Navigation properties
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();

        // Not mapped - computed property
        public Money BasePrice
        {
            get => new Money(BasePriceAmount, BasePriceCurrency);
            set
            {
                BasePriceAmount = value.Amount;
                BasePriceCurrency = value.Currency;
            }
        }

        // Parameterless constructor for EF Core
        public Flight()
        {
        }

        // Constructor for business logic
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

        // Business Logic
        public bool CanBook(int numberOfSeats)
        {
            return Status == FlightStatus.Scheduled
                && AvailableSeats >= numberOfSeats
                && DepartureTime > DateTime.UtcNow.AddHours(2);
        }

        public bool CanBeBooked()
        {
            return CanBook(1);
        }

        public void ReserveSeat(int count)
        {
            if (!CanBook(count))
                throw new InvalidOperationException("Cannot reserve seats for this flight");

            AvailableSeats -= count;
        }

        public void ReleaseSeat(int count)
        {
            AvailableSeats += count;

            if (AvailableSeats > TotalSeats)
                AvailableSeats = TotalSeats;
        }

        public void CancelFlight()
        {
            Status = FlightStatus.Cancelled;
        }

        public TimeSpan GetDuration()
        {
            return ArrivalTime - DepartureTime;
        }

        public int DurationMinutes => (int)GetDuration().TotalMinutes;

        public bool IsInternational()
        {
            return Origin != Destination && GetDuration() > TimeSpan.FromHours(3);
        }
    }
}