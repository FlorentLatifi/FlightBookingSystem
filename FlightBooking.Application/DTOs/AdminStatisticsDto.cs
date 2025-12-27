
using System;

namespace FlightBooking.Application.DTOs
{
    /// <summary>
    /// DTO për statistikat e admin panel
    /// </summary>
    public class AdminStatisticsDto
    {
        public int TotalFlights { get; set; }
        public int TotalReservations { get; set; }
        public int ConfirmedReservations { get; set; }
        public int PendingReservations { get; set; }
        public int CancelledReservations { get; set; }
        public int TotalPassengers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int AvailableSeats { get; set; }
        public int BookedSeats { get; set; }

        /// <summary>
        /// Statistikat për grafikë (optional)
        /// </summary>
        public Dictionary<string, int> ReservationsByStatus { get; set; } = new();
        public Dictionary<string, decimal> RevenueByMonth { get; set; } = new();
    }
}