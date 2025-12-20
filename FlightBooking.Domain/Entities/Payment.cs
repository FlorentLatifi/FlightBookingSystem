using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një pagesë
    /// </summary>
    public class Payment
    {
        /// <summary>
        /// ID unik i pagesës
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID e rezervimit për të cilin është kjo pagesë
        /// Foreign Key
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// Rezervimi i lidhur me këtë pagesë
        /// Navigation property
        /// </summary>
        public virtual Reservation Reservation { get; set; } = null!;

        /// <summary>
        /// Shuma e paguar (në Euro)
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Metoda e pagesës (p.sh. "Credit Card", "PayPal", "Bank Transfer")
        /// </summary>
        public string PaymentMethod { get; set; } = string.Empty;

        /// <summary>
        /// ID e transaksionit nga payment gateway
        /// (p.sh. ID nga Stripe, PayPal, etj.)
        /// </summary>
        public string TransactionId { get; set; } = string.Empty;

        /// <summary>
        /// Statusi i pagesës
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <summary>
        /// Data dhe koha kur është bërë pagesa
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// Data dhe koha kur pagesa u procesua (mund të jetë e ndryshme nga PaymentDate)
        /// </summary>
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Mesazh nga payment gateway (për gabime ose konfirmime)
        /// </summary>
        public string? PaymentGatewayResponse { get; set; }

        /// <summary>
        /// A është pagesa e suksesshme?
        /// </summary>
        public bool IsSuccessful()
        {
            return Status == PaymentStatus.Completed;
        }

        /// <summary>
        /// A mund të rimburssohet kjo pagesë?
        /// </summary>
        public bool CanBeRefunded()
        {
            return Status == PaymentStatus.Completed
                   && Reservation.CanBeCancelled();
        }
    }
}
