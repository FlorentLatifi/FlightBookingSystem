using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Application.Validators
{
    /// <summary>
    /// Rezultati i validimit - përdoret nga të gjithë validators
    /// DESIGN PATTERN: Result Object Pattern
    /// </summary>
    public class ValidationResult
    {
        private readonly Dictionary<string, List<string>> _errors = new();

        /// <summary>
        /// A është validimi i suksesshëm?
        /// </summary>
        public bool IsValid => _errors.Count == 0;

        /// <summary>
        /// Të gjitha errors e grupuara sipas property name
        /// </summary>
        public IReadOnlyDictionary<string, List<string>> Errors => _errors;

        /// <summary>
        /// Shto një error për një property specifik
        /// </summary>
        public void AddError(string propertyName, string errorMessage)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentException("Property name cannot be empty", nameof(propertyName));

            if (string.IsNullOrWhiteSpace(errorMessage))
                throw new ArgumentException("Error message cannot be empty", nameof(errorMessage));

            if (!_errors.ContainsKey(propertyName))
            {
                _errors[propertyName] = new List<string>();
            }

            _errors[propertyName].Add(errorMessage);
        }

        /// <summary>
        /// Merr të gjitha errors për një property specifik
        /// </summary>
        public List<string> GetErrors(string propertyName)
        {
            return _errors.ContainsKey(propertyName)
                ? _errors[propertyName]
                : new List<string>();
        }

        /// <summary>
        /// Merr error-in e parë për një property specifik
        /// </summary>
        public string GetFirstError(string propertyName)
        {
            var errors = GetErrors(propertyName);
            return errors.Count > 0 ? errors[0] : string.Empty;
        }

        /// <summary>
        /// Merr të gjitha errors si një listë të sheshtë
        /// </summary>
        public List<string> GetAllErrors()
        {
            var allErrors = new List<string>();
            foreach (var errorList in _errors.Values)
            {
                allErrors.AddRange(errorList);
            }
            return allErrors;
        }

        /// <summary>
        /// Merr një summary të errors në një string
        /// </summary>
        public string GetErrorSummary()
        {
            if (IsValid) return "No errors";

            var summary = new System.Text.StringBuilder();
            foreach (var kvp in _errors)
            {
                summary.AppendLine($"{kvp.Key}:");
                foreach (var error in kvp.Value)
                {
                    summary.AppendLine($"  - {error}");
                }
            }
            return summary.ToString();
        }

        /// <summary>
        /// Shto errors nga një ValidationResult tjetër
        /// </summary>
        public void AddErrors(ValidationResult other)
        {
            if (other == null) return;

            foreach (var kvp in other._errors)
            {
                foreach (var error in kvp.Value)
                {
                    AddError(kvp.Key, error);
                }
            }
        }
    }
}