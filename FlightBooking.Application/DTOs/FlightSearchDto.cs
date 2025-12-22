using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për kërkimin e fluturimeve
    /// Përdoret kur përdoruesi kërkon fluturime
    /// </summary>
    public class FlightSearchDto
    {
        /// <summary>
        /// Aeroporti i nisjes
        /// </summary>
        [Required(ErrorMessage = "Aeroporti i nisjes është i detyrueshëm")]
        public string DepartureAirport { get; set; } = string.Empty;

        /// <summary>
        /// Aeroporti i mbërritjes
        /// </summary>
        [Required(ErrorMessage = "Aeroporti i mbërritjes është i detyrueshëm")]
        public string ArrivalAirport { get; set; } = string.Empty;

        /// <summary>
        /// Data e nisjes
        /// </summary>
        [Required(ErrorMessage = "Data e nisjes është e detyrueshme")]
        [DataType(DataType.Date)]
        public DateTime DepartureDate { get; set; }

        /// <summary>
        /// Numri i pasagjerëve (opsionale, default 1)
        /// </summary>
        [Range(1, 10, ErrorMessage = "Numri i pasagjerëve duhet të jetë midis 1 dhe 10")]
        public int NumberOfPassengers { get; set; } = 1;
    }
}