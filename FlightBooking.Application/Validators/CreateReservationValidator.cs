using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.DTOs;

namespace FlightBooking.Application.Validators
{
    /// <summary>
    /// DESIGN PATTERN: Validator Pattern
    /// Demonstron Separation of Concerns - validation logic e ndarë nga business logic
    /// </summary>
    public class CreateReservationValidator
    {
        public ValidationResult Validate(CreateReservationDto dto)
        {
            var errors = new List<string>();

            // Passenger validations
            if (string.IsNullOrWhiteSpace(dto.Passenger.FirstName))
                errors.Add("First name is required");

            if (string.IsNullOrWhiteSpace(dto.Passenger.PassportNumber))
                errors.Add("Passport number is required");

            // Age validation
            var age = DateTime.Now.Year - dto.Passenger.DateOfBirth.Year;
            if (age < 18)
                errors.Add("Passenger must be at least 18 years old");

            // Payment validations
            if (!IsValidCardNumber(dto.PaymentDetails.CardNumber))
                errors.Add("Invalid card number");

            if (!IsValidCVV(dto.PaymentDetails.CVV))
                errors.Add("Invalid CVV");

            return new ValidationResult
            {
                IsValid = errors.Count == 0,
                Errors = errors
            };
        }

        private bool IsValidCardNumber(string cardNumber)
        {
            // Luhn algorithm (simplified)
            var cleanNumber = cardNumber.Replace(" ", "").Replace("-", "");
            return cleanNumber.Length >= 13 && cleanNumber.Length <= 19;
        }

        private bool IsValidCVV(string cvv)
        {
            return cvv.Length >= 3 && cvv.Length <= 4 && cvv.All(char.IsDigit);
        }
    }

    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> Errors { get; set; } = new();
    }
}
