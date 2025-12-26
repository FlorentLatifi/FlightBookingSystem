using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Infrastructure.Data.Seed
{
    public static class ReservationSeeder
    {
        public static Reservation[] GetReservations()
        {
            var now = DateTime.UtcNow;

            return new[]
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
                    TotalPrice = 224.98m,
                    Status = ReservationStatus.Pending,
                    ReservationDate = now.AddDays(-3)
                }
            };
        }
    }
}

