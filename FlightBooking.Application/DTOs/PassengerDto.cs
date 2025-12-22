using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për të dhënat e pasagjerit
    /// </summary>
    public class PassengerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Emri është i detyrueshëm")]
        [StringLength(50, ErrorMessage = "Emri nuk mund të jetë më i gjatë se 50 karaktere")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mbiemri është i detyrueshëm")]
        [StringLength(50, ErrorMessage = "Mbiemri nuk mund të jetë më i gjatë se 50 karaktere")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email është i detyrueshëm")]
        [EmailAddress(ErrorMessage = "Email adresa nuk është e vlefshme")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numri i telefonit është i detyrueshëm")]
        [Phone(ErrorMessage = "Numri i telefonit nuk është i vlefshëm")]
        public string PhoneNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Numri i pasaportës është i detyrueshëm")]
        [StringLength(20, ErrorMessage = "Numri i pasaportës nuk mund të jetë më i gjatë se 20 karaktere")]
        public string PassportNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data e lindjes është e detyrueshme")]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Kombësia është e detyrueshme")]
        [StringLength(50)]
        public string Nationality { get; set; } = string.Empty;

        /// <summary>
        /// Emri i plotë
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}
