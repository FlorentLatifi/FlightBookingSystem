using System;
using FlightBooking.Domain.ValueObjects;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    public class Seat
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; } = null!;
        public string SeatNumberValue { get; set; } = string.Empty; // For EF Core
        public SeatClass Class { get; set; }
        public bool IsAvailable { get; set; }
        public int? BookingId { get; set; }
        public virtual Booking? Booking { get; set; }

        // Not mapped
        public SeatNumber SeatNumber
        {
            get => SeatNumber.Parse(SeatNumberValue);
            set => SeatNumberValue = value.ToString();
        }

        // Parameterless constructor for EF Core
        public Seat()
        {
            IsAvailable = true;
        }

        public Seat(Flight flight, SeatNumber seatNumber, SeatClass seatClass)
        {
            Flight = flight ?? throw new ArgumentNullException(nameof(flight));
            FlightId = flight.Id;
            SeatNumber = seatNumber ?? throw new ArgumentNullException(nameof(seatNumber));
            Class = seatClass;
            IsAvailable = true;
        }

        public void Reserve(Booking booking)
        {
            if (!IsAvailable)
                throw new InvalidOperationException($"Seat {SeatNumber} is already reserved");

            IsAvailable = false;
            Booking = booking;
            BookingId = booking.Id;
        }

        public void Release()
        {
            IsAvailable = true;
            Booking = null;
            BookingId = null;
        }

        public Money GetPrice(Money basePrice)
        {
            decimal multiplier = Class switch
            {
                SeatClass.Economy => 1.0m,
                SeatClass.Business => 2.5m,
                SeatClass.FirstClass => 4.0m,
                _ => 1.0m
            };

            return basePrice * multiplier;
        }
    }
}