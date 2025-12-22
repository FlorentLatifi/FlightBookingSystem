using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për të shfaqur detajet e një rezervimi
    /// </summary>
    public class ReservationDto
    {
        public int Id { get; set; }
        public string ReservationCode { get; set; } = string.Empty;
        public FlightDto Flight { get; set; } = new();
        public PassengerDto Passenger { get; set; } = new();
        public SeatClass SeatClass { get; set; }
        public string SeatNumber { get; set; } = string.Empty;
        public decimal TotalPrice { get; set; }
        public ReservationStatus Status { get; set; }
        public DateTime ReservationDate { get; set; }

        /// <summary>
        /// Statusi në format të lexueshëm
        /// </summary>
        public string StatusText
        {
            get
            {
                return Status switch
                {
                    ReservationStatus.Pending => "Në pritje",
                    ReservationStatus.Confirmed => "Konfirmuar",
                    ReservationStatus.Cancelled => "Anuluar",
                    ReservationStatus.CheckedIn => "Check-in i bërë",
                    ReservationStatus.Completed => "Përfunduar",
                    _ => "I panjohur"
                };
            }
        }
    }
}
