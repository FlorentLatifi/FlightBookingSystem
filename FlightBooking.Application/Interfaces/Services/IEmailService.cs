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
    /// </summary>

    public interface IEmailService
    {
        Task SendEmailAsync(string to, string subject, string body);
    }
}