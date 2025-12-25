using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Application.DTOs;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Mappers
{
    /// <summary>
    /// DESIGN PATTERN: Mapper Pattern
    /// Transformon Domain Entities në DTOs për transferim të dhënash
    /// Separation of Concerns - Domain layer nuk varet nga DTOs
    /// </summary>
    public static class EntityToDtoMapper
    {
        /// <summary>
        /// Transformon Flight entity në FlightDto
        /// </summary>
        public static FlightDto ToDto(this Flight flight)
        {
            if (flight == null)
                throw new ArgumentNullException(nameof(flight));

            return new FlightDto
            {
                Id = flight.Id,
                FlightNumber = flight.FlightNumber,
                Airline = flight.AirlineEntity?.Name ?? flight.Airline,
                DepartureAirport = flight.DepartureAirportEntity?.Name ?? flight.DepartureAirport,
                ArrivalAirport = flight.ArrivalAirportEntity?.Name ?? flight.ArrivalAirport,
                DepartureTime = flight.DepartureTime,
                ArrivalTime = flight.ArrivalTime,
                BasePrice = flight.BasePriceAmount,
                AvailableSeats = flight.AvailableSeats,
                Status = flight.Status,
                DurationMinutes = flight.DurationMinutes
            };
        }

        /// <summary>
        /// Transformon koleksion Flights në FlightDtos
        /// </summary>
        public static IEnumerable<FlightDto> ToDtos(this IEnumerable<Flight> flights)
        {
            return flights.Select(f => f.ToDto());
        }

        /// <summary>
        /// Transformon Reservation entity në ReservationDto
        /// </summary>
        public static ReservationDto ToDto(this Reservation reservation)
        {
            if (reservation == null)
                throw new ArgumentNullException(nameof(reservation));

            return new ReservationDto
            {
                Id = reservation.Id,
                ReservationCode = reservation.ReservationCode,
                Flight = reservation.Flight?.ToDto(),
                Passenger = reservation.Passenger != null ? new PassengerDto
                {
                    FirstName = reservation.Passenger.FirstName,
                    LastName = reservation.Passenger.LastName,
                    Email = reservation.Passenger.Email,
                    PhoneNumber = reservation.Passenger.PhoneNumber
                } : null,
                SeatClass = reservation.SeatClass,
                SeatNumber = reservation.SeatNumber,
                TotalPrice = reservation.TotalPrice,
                Status = reservation.Status,
                ReservationDate = reservation.ReservationDate
            };
        }

        /// <summary>
        /// Transformon Payment entity në PaymentDto
        /// </summary>
        public static PaymentDto ToDto(this Payment payment)
        {
            if (payment == null)
                throw new ArgumentNullException(nameof(payment));

            return new PaymentDto
            {
                Id = payment.Id,
                ReservationId = payment.ReservationId,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                TransactionId = payment.TransactionId,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate
            };
        }
    }
}
