using System;
using System.Collections.Generic;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një përdorues të sistemit (admin, staff, etj.)
    /// Përdoret për autentifikim dhe autorizim
    /// </summary>
    public class User
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Emri i përdoruesit
        /// </summary>
        public string Username { get; set; } = string.Empty;
        
        /// <summary>
        /// Email adresa
        /// </summary>
        public string Email { get; set; } = string.Empty;
        
        /// <summary>
        /// Hash i fjalëkalimit
        /// </summary>
        public string PasswordHash { get; set; } = string.Empty;
        
        /// <summary>
        /// Emri i plotë
        /// </summary>
        public string FullName { get; set; } = string.Empty;
        
        /// <summary>
        /// Roli i përdoruesit (Admin, Staff, Customer)
        /// </summary>
        public string Role { get; set; } = "Customer";
        
        /// <summary>
        /// A është përdoruesi aktiv?
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Data e krijimit
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        /// <summary>
        /// Data e përditësimit të fundit
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        
        /// <summary>
        /// Rezervimet e këtij përdoruesi (nëse është customer)
        /// </summary>
        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}
