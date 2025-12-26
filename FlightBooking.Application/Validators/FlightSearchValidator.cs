using System;
using FlightBooking.Application.DTOs;

namespace FlightBooking.Application.Validators
{
    /// <summary>
    /// Validator për FlightSearchDto
    /// Validon të dhënat e kërkimit të fluturimeve
    /// </summary>
    public class FlightSearchValidator
    {
        /// <summary>
        /// Validon DTO-në e kërkimit
        /// </summary>
        public ValidationResult Validate(FlightSearchDto dto)
        {
            var result = new ValidationResult();

            // Valido DepartureAirport
            if (string.IsNullOrWhiteSpace(dto.DepartureAirport))
            {
                result.AddError(nameof(dto.DepartureAirport), "Departure airport is required");
            }
            else if (dto.DepartureAirport.Length < 3)
            {
                result.AddError(nameof(dto.DepartureAirport), "Departure airport must be at least 3 characters");
            }

            // Valido ArrivalAirport
            if (string.IsNullOrWhiteSpace(dto.ArrivalAirport))
            {
                result.AddError(nameof(dto.ArrivalAirport), "Arrival airport is required");
            }
            else if (dto.ArrivalAirport.Length < 3)
            {
                result.AddError(nameof(dto.ArrivalAirport), "Arrival airport must be at least 3 characters");
            }

            // Valido që nisja dhe mbërritja të mos jenë të njëjta
            if (!string.IsNullOrWhiteSpace(dto.DepartureAirport) &&
                !string.IsNullOrWhiteSpace(dto.ArrivalAirport) &&
                dto.DepartureAirport.Equals(dto.ArrivalAirport, StringComparison.OrdinalIgnoreCase))
            {
                result.AddError(nameof(dto.ArrivalAirport), "Arrival airport cannot be the same as departure airport");
            }

            // Valido DepartureDate
            if (dto.DepartureDate.Date < DateTime.Now.Date)
            {
                result.AddError(nameof(dto.DepartureDate), "Departure date cannot be in the past");
            }
            else if (dto.DepartureDate.Date > DateTime.Now.Date.AddYears(1))
            {
                result.AddError(nameof(dto.DepartureDate), "Departure date cannot be more than 1 year in the future");
            }

            // Valido NumberOfPassengers
            if (dto.NumberOfPassengers < 1)
            {
                result.AddError(nameof(dto.NumberOfPassengers), "Number of passengers must be at least 1");
            }
            else if (dto.NumberOfPassengers > 10)
            {
                result.AddError(nameof(dto.NumberOfPassengers), "Number of passengers cannot exceed 10");
            }

            return result;
        }
    }

    /// <summary>
    /// Klasa për rezultatin e validimit
    /// </summary>
    public class ValidationResult
    {
        private readonly Dictionary<string, List<string>> _errors = new();

        public bool IsValid => _errors.Count == 0;

        public Dictionary<string, List<string>> Errors => _errors;

        public void AddError(string propertyName, string errorMessage)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            _errors[propertyName].Add(errorMessage);
        }

        public List<string> GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName) ? _errors[propertyName] : new List<string>();
        }

        public string GetFirstError(string propertyName)
        {
            var errors = GetErrors(propertyName);
            return errors.Count > 0 ? errors[0] : string.Empty;
        }

        public List<string> GetAllErrors()
        {
            var allErrors = new List<string>();
            foreach (var errorList in _errors.Values)
            {
                allErrors.AddRange(errorList);
            }
            return allErrors;
        }
    }
}