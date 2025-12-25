using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    public class Booking
    {
        public int Id { get; set; }
        public string BookingReference { get; set; } = string.Empty;
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; } = null!;
        public string PassengerName { get; set; } = string.Empty;
        public string PassengerEmail { get; set; } = string.Empty;
        public string PassengerPhone { get; set; } = string.Empty;
        public DateTime BookingDate { get; set; }
        public BookingStatus Status { get; set; }
        public decimal TotalPriceAmount { get; set; }
        public string TotalPriceCurrency { get; set; } = "USD";
        public virtual ICollection<Seat> Seats { get; set; } = new List<Seat>();
        public virtual Payment? Payment { get; set; }

        // Not mapped
        public Money TotalPrice
        {
            get => new Money(TotalPriceAmount, TotalPriceCurrency);
            set
            {
                TotalPriceAmount = value.Amount;
                TotalPriceCurrency = value.Currency;
            }
        }

        // Parameterless constructor for EF Core
        public Booking()
        {
            BookingDate = DateTime.UtcNow;
            Status = BookingStatus.Pending;
        }

        public Booking(
            Flight flight,
            string passengerName,
            string passengerEmail,
            string passengerPhone)
        {
            Flight = flight ?? throw new ArgumentNullException(nameof(flight));
            FlightId = flight.Id;
            PassengerName = passengerName ?? throw new ArgumentNullException(nameof(passengerName));
            PassengerEmail = passengerEmail ?? throw new ArgumentNullException(nameof(passengerEmail));
            PassengerPhone = passengerPhone ?? string.Empty;

            BookingReference = GenerateBookingReference();
            BookingDate = DateTime.UtcNow;
            Status = BookingStatus.Pending;
            TotalPrice = new Money(0);
        }

        private string GenerateBookingReference()
        {
            return $"BK-{DateTime.UtcNow:yyyyMMdd}-{Guid.NewGuid().ToString()[..5].ToUpper()}";
        }

        public void AddSeat(Seat seat)
        {
            if (seat.FlightId != FlightId)
                throw new InvalidOperationException("Seat belongs to different flight");

            seat.Reserve(this);
            Seats.Add(seat);
            RecalculateTotalPrice();
        }

        private void RecalculateTotalPrice()
        {
            Money total = new Money(0);
            foreach (var seat in Seats)
            {
                total = total + seat.GetPrice(Flight.BasePrice);
            }
            TotalPrice = total;
        }

        public void Confirm()
        {
            if (Status != BookingStatus.Pending)
                throw new InvalidOperationException("Can only confirm pending bookings");

            Status = BookingStatus.Confirmed;
        }

        public void Cancel()
        {
            if (Status == BookingStatus.Cancelled)
                return;

            Status = BookingStatus.Cancelled;

            foreach (var seat in Seats)
            {
                seat.Release();
            }

            Flight.ReleaseSeat(Seats.Count);
        }

        public bool CanBeCancelled()
        {
            return Flight.DepartureTime > DateTime.UtcNow.AddHours(24)
                && Status != BookingStatus.Cancelled;
        }
    }
}