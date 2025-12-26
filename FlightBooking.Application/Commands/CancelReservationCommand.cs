using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Commands
{
    /// <summary>
    /// Command për anulimin e një rezervimi
    /// CQRS Pattern - Write Operation
    /// </summary>
    public class CancelReservationCommand
    {
        public int ReservationId { get; set; }
        public string? CancellationReason { get; set; }

        public CancelReservationCommand()
        {
        }

        public CancelReservationCommand(int reservationId, string? cancellationReason = null)
        {
            ReservationId = reservationId;
            CancellationReason = cancellationReason;
        }
    }

    /// <summary>
    /// Result i anulimit të rezervimit
    /// </summary>
    public class CancelReservationResult
    {
        public bool Success { get; set; }
        public Reservation? Reservation { get; set; }
        public string? ErrorMessage { get; set; }
        public bool RefundProcessed { get; set; }

        public static CancelReservationResult Successful(Reservation reservation, bool refundProcessed = false)
        {
            return new CancelReservationResult
            {
                Success = true,
                Reservation = reservation,
                RefundProcessed = refundProcessed
            };
        }

        public static CancelReservationResult Failed(string errorMessage)
        {
            return new CancelReservationResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }

    /// <summary>
    /// Handler për CancelReservationCommand
    /// </summary>
    public class CancelReservationCommandHandler
    {
        private readonly IReservationService _reservationService;
        private readonly IReservationRepository _reservationRepository;
        private readonly IPaymentService _paymentService;
        private readonly INotificationService _notificationService;

        public CancelReservationCommandHandler(
            IReservationService reservationService,
            IReservationRepository reservationRepository,
            IPaymentService paymentService,
            INotificationService notificationService)
        {
            _reservationService = reservationService;
            _reservationRepository = reservationRepository;
            _paymentService = paymentService;
            _notificationService = notificationService;
        }

        public async Task<CancelReservationResult> Handle(CancelReservationCommand command)
        {
            try
            {
                // Merr rezervimin
                var reservation = await _reservationRepository.GetByIdAsync(command.ReservationId);
                if (reservation == null)
                {
                    return CancelReservationResult.Failed($"Reservation with ID {command.ReservationId} not found");
                }

                // Kontrollo nëse mund të anulohet
                if (!reservation.CanBeCancelled())
                {
                    return CancelReservationResult.Failed("This reservation cannot be cancelled");
                }

                // Anulo rezervimin
                await _reservationService.CancelReservationAsync(command.ReservationId);

                // Përpuno rimbursimin nëse ka pagesë
                bool refundProcessed = false;
                if (reservation.Payment != null && reservation.Payment.Status == Domain.Enums.PaymentStatus.Completed)
                {
                    refundProcessed = await _paymentService.RefundPaymentAsync(reservation.Payment.Id);
                }

                // Dërgo njoftim anulimi
                await _notificationService.SendCancellationNotificationAsync(reservation);

                return CancelReservationResult.Successful(reservation, refundProcessed);
            }
            catch (Exception ex)
            {
                return CancelReservationResult.Failed($"Error cancelling reservation: {ex.Message}");
            }
        }
    }
}
