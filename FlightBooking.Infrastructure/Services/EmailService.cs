using System;
using System.Threading.Tasks;
using FlightBooking.Application.Interfaces;
using FlightBooking.Application.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FlightBooking.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> _logger;

        public EmailService(ILogger<EmailService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            // TODO: Implement actual email sending (SMTP, SendGrid, etc.)
            _logger.LogInformation($"Sending email to {to}: {subject}");
            await Task.Delay(100); // Simulate sending
            _logger.LogInformation("Email sent successfully");
        }
    }
}