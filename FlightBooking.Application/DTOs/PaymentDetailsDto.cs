using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për të dhënat e pagesës
    /// Përmban informacionet e kartës së kreditit
    /// </summary>
    public class PaymentDetailsDto
    {
        [Required(ErrorMessage = "Metoda e pagesës është e detyrueshme")]
        public string PaymentMethod { get; set; } = "Credit Card";

        [Required(ErrorMessage = "Numri i kartës është i detyrueshëm")]
        [CreditCard(ErrorMessage = "Numri i kartës nuk është i vlefshëm")]
        public string CardNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Emri në kartë është i detyrueshëm")]
        [StringLength(100, ErrorMessage = "Emri në kartë nuk mund të jetë më i gjatë se 100 karaktere")]
        public string CardHolderName { get; set; } = string.Empty;

        [Required(ErrorMessage = "CVV është i detyrueshëm")]
        [StringLength(4, MinimumLength = 3, ErrorMessage = "CVV duhet të jetë 3 ose 4 shifra")]
        [RegularExpression(@"^\d{3,4}$", ErrorMessage = "CVV duhet të përmbajë vetëm numra")]
        public string CVV { get; set; } = string.Empty;

        [Required(ErrorMessage = "Data e skadimit është e detyrueshme")]
        [RegularExpression(@"^(0[1-9]|1[0-2])\/\d{2}$", ErrorMessage = "Data e skadimit duhet të jetë në formatin MM/YY")]
        public string ExpiryDate { get; set; } = string.Empty;
    }
}
