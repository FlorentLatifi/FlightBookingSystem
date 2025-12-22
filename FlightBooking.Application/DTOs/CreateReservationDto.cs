using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using FlightBooking.Domain.Enums;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për krijimin e një rezervimi të ri
    /// Përmban të gjitha informacionet e nevojshme për rezervim
    /// </summary>
    public class CreateReservationDto
    {
        /// <summary>
        /// ID e fluturimit që rezervohet
        /// </summary>
        [Required]
        public int FlightId { get; set; }

        /// <summary>
        /// Të dhënat e pasagjerit
        /// </summary>
        [Required]
        public PassengerDto Passenger { get; set; } = new();

        /// <summary>
        /// Klasa e ulëses
        /// </summary>
        [Required]
        public SeatClass SeatClass { get; set; }

        /// <summary>
        /// Të dhënat e pagesës
        /// </summary>
        [Required]
        public PaymentDetailsDto PaymentDetails { get; set; } = new();
    }
}
