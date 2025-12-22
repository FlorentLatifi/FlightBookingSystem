using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Services;

namespace FlightBooking.Infrastructure.Services
{
    /// <summary>
    /// Implementimi i IEmailService
    /// SHËNIM: Ky është një MOCK implementation për demonstrim
    /// Në një aplikacion real, do të integrohej me një email provider (SendGrid, SMTP, etc.)
    /// </summary>
    public class EmailService : IEmailService
    {
        /// <summary>
        /// Dërgon një email
        /// </summary>
        public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
        {
            // Simulojmë vonesën e dërgimit
            await Task.Delay(500);

            // Log në console për demonstrim
            Console.WriteLine("===============================================");
            Console.WriteLine("[EMAIL SERVICE] Email u dërgua me sukses!");
            Console.WriteLine("===============================================");
            Console.WriteLine($"Nga:      noreply@flightbooking.com");
            Console.WriteLine($"Për:      {toEmail}");
            Console.WriteLine($"Subjekti: {subject}");
            Console.WriteLine($"Format:   {(isHtml ? "HTML" : "Plain Text")}");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("PËRMBAJTJA:");
            Console.WriteLine(body);
            Console.WriteLine("===============================================\n");

            // Në një aplikacion real, këtu do të thërritej email provider:
            /*
            using var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new NetworkCredential("user@example.com", "password");
            smtpClient.EnableSsl = true;
            
            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@flightbooking.com"),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml
            };
            mailMessage.To.Add(toEmail);
            
            await smtpClient.SendMailAsync(mailMessage);
            */
        }

        /// <summary>
        /// Dërgon email me attachment
        /// </summary>
        public async Task SendEmailWithAttachmentAsync(
            string toEmail,
            string subject,
            string body,
            byte[] attachment,
            string attachmentName)
        {
            // Simulojmë vonesën e dërgimit
            await Task.Delay(800);

            // Log në console për demonstrim
            Console.WriteLine("===============================================");
            Console.WriteLine("[EMAIL SERVICE] Email me attachment u dërgua!");
            Console.WriteLine("===============================================");
            Console.WriteLine($"Nga:        noreply@flightbooking.com");
            Console.WriteLine($"Për:        {toEmail}");
            Console.WriteLine($"Subjekti:   {subject}");
            Console.WriteLine($"Attachment: {attachmentName} ({attachment.Length} bytes)");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("PËRMBAJTJA:");
            Console.WriteLine(body);
            Console.WriteLine("===============================================\n");

            // Në një aplikacion real, këtu do të shtohej attachment në email
        }
    }
}
