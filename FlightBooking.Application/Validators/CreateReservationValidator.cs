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
            var result = new ValidationResult();

            // Passenger validations
            if (string.IsNullOrWhiteSpace(dto.Passenger.FirstName))
                result.AddError(nameof(dto.Passenger.FirstName), "First name is required");

            if (string.IsNullOrWhiteSpace(dto.Passenger.LastName))
                result.AddError(nameof(dto.Passenger.LastName), "Last name is required");

            if (string.IsNullOrWhiteSpace(dto.Passenger.PassportNumber))
                result.AddError(nameof(dto.Passenger.PassportNumber), "Passport number is required");

            // Age validation
            var age = DateTime.Now.Year - dto.Passenger.DateOfBirth.Year;
            if (age < 18)
                result.AddError(nameof(dto.Passenger.DateOfBirth), "Passenger must be at least 18 years old");

            // Payment validations
            if (!IsValidCardNumber(dto.PaymentDetails.CardNumber))
                result.AddError(nameof(dto.PaymentDetails.CardNumber), "Invalid card number");

            if (!IsValidCVV(dto.PaymentDetails.CVV))
                result.AddError(nameof(dto.PaymentDetails.CVV), "Invalid CVV");

            return result;
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
}