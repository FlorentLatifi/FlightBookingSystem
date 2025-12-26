using System;
using System.Text.RegularExpressions;
using FlightBooking.Application.DTOs;

namespace FlightBooking.Application.Validators
{
    /// <summary>
    /// Validator për PaymentDetailsDto
    /// Validon të dhënat e pagesës (kartelë krediti)
    /// </summary>
    public class PaymentDetailsValidator
    {
        /// <summary>
        /// Validon të dhënat e pagesës
        /// </summary>
        public ValidationResult Validate(PaymentDetailsDto dto)
        {
            var result = new ValidationResult();

            // Valido PaymentMethod
            if (string.IsNullOrWhiteSpace(dto.PaymentMethod))
            {
                result.AddError(nameof(dto.PaymentMethod), "Payment method is required");
            }

            // Valido CardNumber
            if (string.IsNullOrWhiteSpace(dto.CardNumber))
            {
                result.AddError(nameof(dto.CardNumber), "Card number is required");
            }
            else
            {
                var cleanCardNumber = dto.CardNumber.Replace(" ", "").Replace("-", "");

                if (!IsValidCardNumber(cleanCardNumber))
                {
                    result.AddError(nameof(dto.CardNumber), "Card number is not valid");
                }

                if (cleanCardNumber.Length < 13 || cleanCardNumber.Length > 19)
                {
                    result.AddError(nameof(dto.CardNumber), "Card number must be between 13 and 19 digits");
                }
            }

            // Valido CardHolderName
            if (string.IsNullOrWhiteSpace(dto.CardHolderName))
            {
                result.AddError(nameof(dto.CardHolderName), "Cardholder name is required");
            }
            else if (dto.CardHolderName.Length < 3)
            {
                result.AddError(nameof(dto.CardHolderName), "Cardholder name must be at least 3 characters");
            }
            else if (dto.CardHolderName.Length > 100)
            {
                result.AddError(nameof(dto.CardHolderName), "Cardholder name cannot exceed 100 characters");
            }

            // Valido CVV
            if (string.IsNullOrWhiteSpace(dto.CVV))
            {
                result.AddError(nameof(dto.CVV), "CVV is required");
            }
            else if (!Regex.IsMatch(dto.CVV, @"^\d{3,4}$"))
            {
                result.AddError(nameof(dto.CVV), "CVV must be 3 or 4 digits");
            }

            // Valido ExpiryDate
            if (string.IsNullOrWhiteSpace(dto.ExpiryDate))
            {
                result.AddError(nameof(dto.ExpiryDate), "Expiry date is required");
            }
            else if (!Regex.IsMatch(dto.ExpiryDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
            {
                result.AddError(nameof(dto.ExpiryDate), "Expiry date must be in MM/YY format");
            }
            else
            {
                // Kontrollo që karta të mos jetë e skaduar
                if (!IsCardExpired(dto.ExpiryDate))
                {
                    result.AddError(nameof(dto.ExpiryDate), "Card has expired");
                }
            }

            return result;
        }

        /// <summary>
        /// Validon numrin e kartelës duke përdorur Luhn Algorithm
        /// </summary>
        private bool IsValidCardNumber(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
                return false;

            if (!Regex.IsMatch(cardNumber, @"^\d+$"))
                return false;

            // Luhn Algorithm (Mod 10)
            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int digit = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9)
                    {
                        digit -= 9;
                    }
                }

                sum += digit;
                alternate = !alternate;
            }

            return (sum % 10) == 0;
        }

        /// <summary>
        /// Kontrollon nëse karta është e skaduar
        /// </summary>
        private bool IsCardExpired(string expiryDate)
        {
            if (!Regex.IsMatch(expiryDate, @"^(0[1-9]|1[0-2])\/\d{2}$"))
                return false;

            var parts = expiryDate.Split('/');
            var month = int.Parse(parts[0]);
            var year = int.Parse("20" + parts[1]); // Supozon 20XX

            var expiryDateTime = new DateTime(year, month, 1).AddMonths(1).AddDays(-1);

            return expiryDateTime >= DateTime.Now;
        }
    }
}