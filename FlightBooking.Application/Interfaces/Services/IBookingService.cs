using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Services
{
    public interface IBookingService
    {
        Task<Booking> CreateBookingAsync(
            int flightId,
            List<string> seatNumbers,
            string passengerName,
            string passengerEmail,
            string passengerPhone,
            string pricingStrategy = "standard");

        Task<Booking> ConfirmBookingAsync(int bookingId);
        Task CancelBookingAsync(int bookingId);
        Task<Booking?> GetBookingByIdAsync(int bookingId);
        Task<Booking?> GetBookingByReferenceAsync(string bookingReference);
    }

}
