using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Application.Interfaces.Services
{
    /// <summary>
    /// Interface për dërgimin e email-ave
    /// Implementimi do të jetë në Infrastructure layer
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Dërgon një email
        /// </summary>
        Task SendEmailAsync(
            string toEmail,
            string subject,
            string body,
            bool isHtml = true);

        /// <summary>
        /// Dërgon email me attachment
        /// </summary>
        Task SendEmailWithAttachmentAsync(
            string toEmail,
            string subject,
            string body,
            byte[] attachment,
            string attachmentName);
    }
}