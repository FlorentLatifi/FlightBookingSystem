using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using FlightBooking.Application.Exceptions;
using FlightBooking.Application.Observers;
using FlightBooking.Application.Strategies.Pricing;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;
using FlightBooking.Application.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using FlightBooking.Application.Interfaces.Services;

namespace FlightBooking.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly ISeatRepository _seatRepository;
        private readonly PricingService _pricingService;
        private readonly List<IBookingObserver> _observers;
        private readonly ILogger<BookingService> _logger;

        public BookingService(
            IBookingRepository bookingRepository,
            IFlightRepository flightRepository,
            ISeatRepository seatRepository,
            PricingService pricingService,
            IEnumerable<IBookingObserver> observers,
            ILogger<BookingService> logger)
        {
            _bookingRepository = bookingRepository ?? throw new ArgumentNullException(nameof(bookingRepository));
            _flightRepository = flightRepository ?? throw new ArgumentNullException(nameof(flightRepository));
            _seatRepository = seatRepository ?? throw new ArgumentNullException(nameof(seatRepository));
            _pricingService = pricingService ?? throw new ArgumentNullException(nameof(pricingService));
            _observers = observers?.ToList() ?? new List<IBookingObserver>();
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void Attach(IBookingObserver observer)
        {
            if (!_observers.Contains(observer))
            {
                _observers.Add(observer);
                _logger.LogInformation("Observer attached: {ObserverName}", observer.ObserverName);
            }
        }

        public void Detach(IBookingObserver observer)
        {
            _observers.Remove(observer);
            _logger.LogInformation("Observer detached: {ObserverName}", observer.ObserverName);
        }

        private async Task NotifyAllAsync(BookingNotification notification)
        {
            _logger.LogInformation("Notifying {ObserverCount} observers about {NotificationType}",
                _observers.Count, notification.Type);

            // PARALLEL EXECUTION
            var tasks = _observers.Select(observer => observer.NotifyAsync(notification));
            await Task.WhenAll(tasks);

            _logger.LogInformation("All observers notified successfully");
        }

        public async Task<Booking> CreateBookingAsync(
            int flightId,
            List<string> seatNumbers,
            string passengerName,
            string passengerEmail,
            string passengerPhone,
            string pricingStrategy = "standard")
        {
            var flight = await _flightRepository.GetByIdAsync(flightId);
            if (flight == null)
                throw new NotFoundException("Flight not found");

            if (!flight.CanBook(seatNumbers.Count))
                throw new InvalidOperationException("Cannot book this flight");

            var booking = new Booking(flight, passengerName, passengerEmail, passengerPhone);

            foreach (var seatNumber in seatNumbers)
            {
                var seat = await _seatRepository.GetBySeatNumberAsync(flightId, seatNumber);
                if (seat == null || !seat.IsAvailable)
                    throw new InvalidOperationException($"Seat {seatNumber} is not available");

                booking.AddSeat(seat);
            }

            flight.ReserveSeat(seatNumbers.Count);

            await _bookingRepository.AddAsync(booking);
            await _flightRepository.UpdateAsync(flight);

            var notification = new BookingNotification(
                booking,
                NotificationType.BookingCreated,
                "Your booking has been created successfully. Please proceed with payment.");

            await NotifyAllAsync(notification);

            return booking;
        }

        public async Task<Booking> ConfirmBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException("Booking not found");

            booking.Confirm();
            await _bookingRepository.UpdateAsync(booking);

            var notification = new BookingNotification(
                booking,
                NotificationType.BookingConfirmed,
                "Your booking has been confirmed. Have a great flight!");

            await NotifyAllAsync(notification);

            return booking;
        }

        public async Task CancelBookingAsync(int bookingId)
        {
            var booking = await _bookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException("Booking not found");

            if (!booking.CanBeCancelled())
                throw new InvalidOperationException("Booking cannot be cancelled");

            booking.Cancel();
            await _bookingRepository.UpdateAsync(booking);

            var notification = new BookingNotification(
                booking,
                NotificationType.BookingCancelled,
                "Your booking has been cancelled. Refund will be processed shortly.");

            await NotifyAllAsync(notification);
        }

        public async Task<Booking?> GetBookingByIdAsync(int bookingId)
        {
            return await _bookingRepository.GetByIdAsync(bookingId);
        }

        public async Task<Booking?> GetBookingByReferenceAsync(string bookingReference)
        {
            return await _bookingRepository.GetByReferenceAsync(bookingReference);
        }
    }
}