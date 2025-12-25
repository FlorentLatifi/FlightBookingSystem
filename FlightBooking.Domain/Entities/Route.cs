using System;
using System.Collections.Generic;

namespace FlightBooking.Domain.Entities
{
    /// <summary>
    /// Entiteti që përfaqëson një rrugë fluturimi (route)
    /// Përdoret për të menaxhuar rrugët e fluturimeve midis aeroportëve
    /// </summary>
    public class Route
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Aeroporti i nisjes
        /// </summary>
        public int DepartureAirportId { get; set; }
        public virtual Airport DepartureAirport { get; set; } = null!;
        
        /// <summary>
        /// Aeroporti i arritjes
        /// </summary>
        public int ArrivalAirportId { get; set; }
        public virtual Airport ArrivalAirport { get; set; } = null!;
        
        /// <summary>
        /// Distanca në kilometra
        /// </summary>
        public decimal DistanceKm { get; set; }
        
        /// <summary>
        /// Kohëzgjatja mesatare e fluturimit në minuta
        /// </summary>
        public int AverageDurationMinutes { get; set; }
        
        /// <summary>
        /// A është rruga aktive?
        /// </summary>
        public bool IsActive { get; set; } = true;
        
        /// <summary>
        /// Fluturimet që përdorin këtë rrugë
        /// </summary>
        public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();
        
        /// <summary>
        /// Emri i rrugës (p.sh. "PRN-VIE")
        /// </summary>
        public string RouteName => $"{DepartureAirport?.Code}-{ArrivalAirport?.Code}";
    }
}
