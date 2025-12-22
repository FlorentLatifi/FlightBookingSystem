using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// Interface për logjikën e biznesit të pagesave
    /// </summary>
    public interface IPaymentService
    {
        /// <summary>
        /// Procesohet pagesa për një rezervim
        /// Kjo metodë do të komunikojë me payment gateway (Stripe, PayPal, etj.)
        /// </summary>
        Task<Payment> ProcessPaymentAsync(
            int reservationId,
            decimal amount,
            string paymentMethod,
            string cardNumber,
            string cardHolderName,
            string cvv,
            string expiryDate);

        /// <summary>
        /// Verifikon statusin e pagesës
        /// </summary>
        Task<Payment?> GetPaymentStatusAsync(int paymentId);

        /// <summary>
        /// Rimburson një pagesë (kur anulohet rezervimi)
        /// </summary>
        Task<bool> RefundPaymentAsync(int paymentId);
    }
}
