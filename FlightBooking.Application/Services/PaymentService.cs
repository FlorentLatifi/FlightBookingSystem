using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.Services
{
    /// <summary>
    /// Service për logjikën e biznesit të pagesave
    /// </summary>
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IReservationRepository _reservationRepository;

        public PaymentService(
            IPaymentRepository paymentRepository,
            IReservationRepository reservationRepository)
        {
            _paymentRepository = paymentRepository;
            _reservationRepository = reservationRepository;
        }

        /// <summary>
        /// Procesohet pagesa për një rezervim
        /// SIMULON procesimin e pagesës me payment gateway
        /// </summary>
        public async Task<Payment> ProcessPaymentAsync(
            int reservationId,
            decimal amount,
            string paymentMethod,
            string cardNumber,
            string cardHolderName,
            string cvv,
            string expiryDate)
        {
            Console.WriteLine("\n============================================");
            Console.WriteLine("[PaymentService] Duke filluar procesimin e pagesës...");
            Console.WriteLine("============================================");

            // Validimet
            var reservation = await _reservationRepository.GetByIdAsync(reservationId);
            if (reservation == null)
                throw new InvalidOperationException($"Rezervimi me ID {reservationId} nuk u gjet");

            if (reservation.Status != ReservationStatus.Pending)
                throw new InvalidOperationException($"Rezervimi me status {reservation.Status} nuk mund të paguhet");

            if (amount != reservation.TotalPrice)
                throw new InvalidOperationException($"Shuma e pagesës ({amount}) nuk përputhet me çmimin e rezervimit ({reservation.TotalPrice})");

            // Valido kartelën
            ValidateCreditCard(cardNumber, cvv, expiryDate);

            // Krijo pagesën me status Processing
            var payment = new Payment
            {
                ReservationId = reservationId,
                Amount = amount,
                PaymentMethod = paymentMethod,
                Status = PaymentStatus.Processing,
                PaymentDate = DateTime.Now,
                TransactionId = GenerateTransactionId(),
                Reservation = reservation
            };

            Console.WriteLine($"[PaymentService] Pagesa u krijua me Transaction ID: {payment.TransactionId}");
            Console.WriteLine($"[PaymentService] Shuma: €{amount:F2}");
            Console.WriteLine($"[PaymentService] Metoda: {paymentMethod}");
            Console.WriteLine($"[PaymentService] Status: {payment.Status}");

            // Ruaj pagesën
            await _paymentRepository.AddAsync(payment);

            // SIMULOJMË PROCESIMIN E PAGESËS (në realitet do të thërritej payment gateway)
            Console.WriteLine("[PaymentService] Duke komunikuar me payment gateway...");
            await Task.Delay(1000); // Simulon vonesën e API

            // Simulo përgjigjen nga payment gateway
            var paymentSuccess = SimulatePaymentGatewayResponse(cardNumber);

            if (paymentSuccess)
            {
                // Pagesa u krye me sukses
                payment.Status = PaymentStatus.Completed;
                payment.ProcessedDate = DateTime.Now;
                payment.PaymentGatewayResponse = "Payment processed successfully. Authorization code: AUTH-" + DateTime.Now.Ticks;

                Console.WriteLine("[PaymentService] ✅ Pagesa u procesua me SUKSES!");
            }
            else
            {
                // Pagesa dështoi
                payment.Status = PaymentStatus.Failed;
                payment.PaymentGatewayResponse = "Payment failed. Insufficient funds or invalid card.";

                Console.WriteLine("[PaymentService] ❌ Pagesa DËSHTOI!");
            }

            // Përditëso pagesën
            await _paymentRepository.UpdateAsync(payment);

            Console.WriteLine("============================================\n");

            return payment;
        }

        /// <summary>
        /// Verifikon statusin e pagesës
        /// </summary>
        public async Task<Payment?> GetPaymentStatusAsync(int paymentId)
        {
            if (paymentId <= 0)
                throw new ArgumentException("ID e pagesës nuk është e vlefshme", nameof(paymentId));

            return await _paymentRepository.GetByIdAsync(paymentId);
        }

        /// <summary>
        /// Rimburson një pagesë (kur anulohet rezervimi)
        /// </summary>
        public async Task<bool> RefundPaymentAsync(int paymentId)
        {
            Console.WriteLine($"\n[PaymentService] Duke procesuar rimbursimin për pagesën ID: {paymentId}");

            var payment = await _paymentRepository.GetByIdAsync(paymentId);

            if (payment == null)
            {
                Console.WriteLine("[PaymentService] Pagesa nuk u gjet");
                return false;
            }

            if (!payment.CanBeRefunded())
            {
                Console.WriteLine($"[PaymentService] Pagesa me status {payment.Status} nuk mund të rimburssohet");
                return false;
            }

            // SIMULOJMË RIMBURSIMIN (në realitet do të thërritej payment gateway)
            Console.WriteLine("[PaymentService] Duke komunikuar me payment gateway për rimbursin...");
            await Task.Delay(800);

            // Ndrysho statusin në Refunded
            payment.Status = PaymentStatus.Refunded;
            payment.PaymentGatewayResponse += " | Refund processed on " + DateTime.Now;

            await _paymentRepository.UpdateAsync(payment);

            Console.WriteLine($"[PaymentService] ✅ Rimbursi i €{payment.Amount:F2} u procesua me sukses!");

            return true;
        }

        /// <summary>
        /// Valido të dhënat e kartës së kreditit
        /// </summary>
        private void ValidateCreditCard(string cardNumber, string cvv, string expiryDate)
        {
            // Heq hapësirat nga numri i kartës
            cardNumber = cardNumber.Replace(" ", "").Replace("-", "");

            // Validime bazike
            if (cardNumber.Length < 13 || cardNumber.Length > 19)
                throw new ArgumentException("Numri i kartës nuk është i vlefshëm");

            if (cvv.Length < 3 || cvv.Length > 4)
                throw new ArgumentException("CVV nuk është i vlefshëm");

            // Valido datën e skadimit
            if (!DateTime.TryParseExact(expiryDate, "MM/yy", null, System.Globalization.DateTimeStyles.None, out var expiry))
                throw new ArgumentException("Data e skadimit nuk është e vlefshme");

            if (expiry < DateTime.Now)
                throw new ArgumentException("Karta ka skaduar");

            Console.WriteLine("[PaymentService] Validimi i kartës: ✅ Karta është e vlefshme");
        }

        /// <summary>
        /// Gjeneron një Transaction ID unik
        /// </summary>
        private string GenerateTransactionId()
        {
            return "TXN-" + Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();
        }

        /// <summary>
        /// Simulon përgjigjen nga payment gateway
        /// Në realitet, këtu do të thërritej API e payment provider
        /// </summary>
        private bool SimulatePaymentGatewayResponse(string cardNumber)
        {
            // Simulojmë që kartat që përfundojnë me numër tek dështojnë
            // Në realitet, kjo do të ishte përgjigja reale nga payment gateway
            var lastDigit = int.Parse(cardNumber.Substring(cardNumber.Length - 1));
            return lastDigit % 2 == 0; // Numra çift = sukses, tek = dështim
        }
    }
}
