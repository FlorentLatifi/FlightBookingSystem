using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Commands
{
    /// <summary>
    /// Command për procesimin e pagesës
    /// CQRS Pattern - Write Operation
    /// </summary>
    public class ProcessPaymentCommand
    {
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = "Credit Card";
        public string CardNumber { get; set; } = string.Empty;
        public string CardHolderName { get; set; } = string.Empty;
        public string CVV { get; set; } = string.Empty;
        public string ExpiryDate { get; set; } = string.Empty;

        public ProcessPaymentCommand()
        {
        }

        public ProcessPaymentCommand(
            int reservationId,
            decimal amount,
            string paymentMethod,
            string cardNumber,
            string cardHolderName,
            string cvv,
            string expiryDate)
        {
            ReservationId = reservationId;
            Amount = amount;
            PaymentMethod = paymentMethod;
            CardNumber = cardNumber;
            CardHolderName = cardHolderName;
            CVV = cvv;
            ExpiryDate = expiryDate;
        }
    }

    /// <summary>
    /// Result i procesimit të pagesës
    /// </summary>
    public class ProcessPaymentResult
    {
        public bool Success { get; set; }
        public Payment? Payment { get; set; }
        public string? ErrorMessage { get; set; }
        public bool ReservationConfirmed { get; set; }

        public static ProcessPaymentResult Successful(Payment payment, bool reservationConfirmed)
        {
            return new ProcessPaymentResult
            {
                Success = true,
                Payment = payment,
                ReservationConfirmed = reservationConfirmed
            };
        }

        public static ProcessPaymentResult Failed(string errorMessage)
        {
            return new ProcessPaymentResult
            {
                Success = false,
                ErrorMessage = errorMessage
            };
        }
    }

    /// <summary>
    /// Handler për ProcessPaymentCommand
    /// Demonstron PARALLEL PROCESSING (si në provim!)
    /// </summary>
    public class ProcessPaymentCommandHandler
    {
        private readonly IPaymentService _paymentService;
        private readonly IReservationService _reservationService;
        private readonly IReservationRepository _reservationRepository;
        private readonly INotificationService _notificationService;

        public ProcessPaymentCommandHandler(
            IPaymentService paymentService,
            IReservationService reservationService,
            IReservationRepository reservationRepository,
            INotificationService notificationService)
        {
            _paymentService = paymentService;
            _reservationService = reservationService;
            _reservationRepository = reservationRepository;
            _notificationService = notificationService;
        }

        public async Task<ProcessPaymentResult> Handle(ProcessPaymentCommand command)
        {
            try
            {
                // Valido rezervimin
                var reservation = await _reservationRepository.GetByIdAsync(command.ReservationId);
                if (reservation == null)
                {
                    return ProcessPaymentResult.Failed($"Reservation with ID {command.ReservationId} not found");
                }

                if (reservation.Status != Domain.Enums.ReservationStatus.Pending)
                {
                    return ProcessPaymentResult.Failed($"Reservation with status {reservation.Status} cannot be paid");
                }

                // =============================================
                // PARALLEL PROCESSING (si në provim!)
                // Pagesa dhe Njoftimi fillojnë NË TË NJËJTËN KOHË
                // =============================================
                Console.WriteLine("\n🔥 PARALLEL PROCESSING STARTED 🔥");

                // Task 1: Proceso pagesën
                var paymentTask = _paymentService.ProcessPaymentAsync(
                    command.ReservationId,
                    command.Amount,
                    command.PaymentMethod,
                    command.CardNumber,
                    command.CardHolderName,
                    command.CVV,
                    command.ExpiryDate);

                // Task 2: Përgatit njoftimin (por nuk e dërgon ende)
                var notificationPrepTask = Task.Run(async () =>
                {
                    Console.WriteLine("[Parallel Task] Notification preparation started...");
                    await Task.Delay(300); // Simulon përgatitjen
                    Console.WriteLine("[Parallel Task] Notification prepared!");
                });

                // Prit që TË DY tasks të përfundojnë
                await Task.WhenAll(paymentTask, notificationPrepTask);

                Console.WriteLine("🎉 PARALLEL PROCESSING COMPLETED 🎉\n");

                var payment = await paymentTask;

                // Kontrollo statusin e pagesës
                if (payment.IsSuccessful)
                {
                    // Konfirmo rezervimin
                    await _reservationService.ConfirmReservationAsync(command.ReservationId);

                    // Dërgo njoftime (Email + SMS në paralel - OBSERVER PATTERN)
                    await _notificationService.SendReservationConfirmationAsync(reservation);
                    await _notificationService.SendPaymentConfirmationAsync(payment);

                    return ProcessPaymentResult.Successful(payment, true);
                }
                else
                {
                    // Pagesa dështoi - anulo rezervimin
                    await _reservationService.CancelReservationAsync(command.ReservationId);

                    return ProcessPaymentResult.Failed($"Payment failed: {payment.PaymentGatewayResponse}");
                }
            }
            catch (Exception ex)
            {
                return ProcessPaymentResult.Failed($"Error processing payment: {ex.Message}");
            }
        }
    }
}