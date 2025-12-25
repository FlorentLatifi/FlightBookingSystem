using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using FlightBooking.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Infrastructure.Services
{
    public class SmsService : ISmsService
    {
        private readonly ILogger<SmsService> _logger;

        public SmsService(ILogger<SmsService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendSmsAsync(string phoneNumber, string message)
        {
            // TODO: Implement actual SMS sending (Twilio, etc.)
            _logger.LogInformation($"Sending SMS to {phoneNumber}: {message}");
            await Task.Delay(100); // Simulate sending
            _logger.LogInformation("SMS sent successfully");
        }
    }
}
