using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Commands
{
    /// <summary>
    /// Command për krijimin e një rezervimi të ri
    /// CQRS Pattern - Write Operation
    /// </summary>
    public class CreateReservationCommand
    {
        public int FlightId { get; set; }
        public int PassengerId { get; set; }
        public SeatClass SeatClass { get; set; }
        public string? Notes { get; set; }

        public CreateReservationCommand()
        {
        }

        public CreateReservationCommand(int flightId, int passengerId, SeatClass seatClass)
        {
            FlightId = flightId;
            PassengerId = passengerId;
            SeatClass = seatClass;
        }
    }

    /// <summary>
    /// Result i krijimit të rezervimit
    /// </summary>
    public class CreateReservationResult
    {
        public bool Success { get; set; }
        public Reservation? Reservation { get; set; }
        public string? ErrorMessage { get; set; }

        public static CreateReservationResult Successful(Reservation reservation)
        {
            return new CreateReservationResult
            {
                Success = true,
                Reservation = reservation
            };
        }

        public static CreateReservationResult Failed(string errorMessage)
        {
            return new CreateReservationResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }

    /// <summary>
    /// Handler për CreateReservationCommand
    /// Përdor IReservationService për logjikën e biznesit
    /// </summary>
    public class CreateReservationCommandHandler
    {
        private readonly IReservationService _reservationService;
        private readonly IFlightRepository _flightRepository;
        private readonly IPassengerRepository _passengerRepository;

        public CreateReservationCommandHandler(
            IReservationService reservationService,
            IFlightRepository flightRepository,
            IPassengerRepository passengerRepository)
        {
            _reservationService = reservationService;
            _flightRepository = flightRepository;
            _passengerRepository = passengerRepository;
        }

        public async Task<CreateReservationResult> Handle(CreateReservationCommand command)
        {
            try
            {
                // Validime
                var flight = await _flightRepository.GetByIdAsync(command.FlightId);
                if (flight == null)
                {
                    return CreateReservationResult.Failed($"Flight with ID {command.FlightId} not found");
                }

                var passenger = await _passengerRepository.GetByIdAsync(command.PassengerId);
                if (passenger == null)
                {
                    return CreateReservationResult.Failed($"Passenger with ID {command.PassengerId} not found");
                }

                if (!flight.CanBeBooked())
                {
                    return CreateReservationResult.Failed("This flight cannot be booked");
                }

                // Krijo rezervimin përmes service
                var reservation = await _reservationService.CreateReservationAsync(
                    command.FlightId,
                    command.PassengerId,
                    command.SeatClass);

                return CreateReservationResult.Successful(reservation);
            }
            catch (Exception ex)
            {
                return CreateReservationResult.Failed($"Error creating reservation: {ex.Message}");
            }
        }
    }
}
