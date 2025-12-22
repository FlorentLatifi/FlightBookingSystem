using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për të shfaqur detajet e pagesës
    /// </summary>
    public class PaymentDto
    {
        public int Id { get; set; }
        public int ReservationId { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string TransactionId { get; set; } = string.Empty;
        public PaymentStatus Status { get; set; }
        public DateTime PaymentDate { get; set; }
        public DateTime? ProcessedDate { get; set; }

        /// <summary>
        /// Statusi në format të lexueshëm
        /// </summary>
        public string StatusText
        {
            get
            {
                return Status switch
                {
                    PaymentStatus.Pending => "Në pritje",
                    PaymentStatus.Processing => "Duke u procesuar",
                    PaymentStatus.Completed => "E përfunduar",
                    PaymentStatus.Failed => "Dështuar",
                    PaymentStatus.Refunded => "E rimbursuar",
                    _ => "I panjohur"
                };
            }
        }

        /// <summary>
        /// A është pagesa e suksesshme?
        /// </summary>
        public bool IsSuccessful => Status == PaymentStatus.Completed;
    }
}
