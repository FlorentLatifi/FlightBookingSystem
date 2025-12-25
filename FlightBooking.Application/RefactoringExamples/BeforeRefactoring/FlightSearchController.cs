using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightBooking.Domain.Entities;
using FlightBooking.Domain.ValueObjects;

namespace FlightBooking.Application.RefactoringExamples.BeforeRefactoring
{
    // FlightBooking.Application/RefactoringExamples/BeforeRefactoring/FlightSearchController.cs
    // NOTE: This is an example file for refactoring demonstration
    // It uses ASP.NET Core types but is in Application layer for educational purposes
    // In production, controllers should be in Web layer only

    /// <summary>
    /// BEFORE REFACTORING - Bad Smells:
    /// 1. Long Method
    /// 2. Duplicated Code
    /// 3. Feature Envy
    /// 4. Primitive Obsession
    /// 5. No separation of concerns
    /// </summary>
    // COMMENTED OUT - This requires ASP.NET Core references which should not be in Application layer
    /*
    public class FlightSearchController_Old : Controller
    {
        [HttpPost]
        public IActionResult Search(string origin, string destination, string date, int passengers)
        {
            // DUPLICATED CODE - same validation in multiple places
            if (string.IsNullOrEmpty(origin))
            {
                ViewBag.Error = "Origin is required";
                return View();
            }
            if (string.IsNullOrEmpty(destination))
            {
                ViewBag.Error = "Destination is required";
                return View();
            }
            if (string.IsNullOrEmpty(date))
            {
                ViewBag.Error = "Date is required";
                return View();
            }

            // PRIMITIVE OBSESSION - using strings instead of DateTime
            DateTime departureDate;
            try
            {
                departureDate = DateTime.Parse(date);
            }
            catch
            {
                ViewBag.Error = "Invalid date format";
                return View();
            }

            // LONG METHOD - too much logic in controller
            var connectionString = "Server=...";
            var flights = new List<Flight>();

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                var command = new SqlCommand(
                    "SELECT * FROM Flights WHERE Origin = @origin AND Destination = @dest AND CAST(DepartureTime AS DATE) = @date",
                    connection);
                command.Parameters.AddWithValue("@origin", origin);
                command.Parameters.AddWithValue("@dest", destination);
                command.Parameters.AddWithValue("@date", departureDate.Date);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var flight = new Flight
                        {
                            Id = (int)reader["Id"],
                            FlightNumber = reader["FlightNumber"].ToString(),
                            Origin = reader["Origin"].ToString(),
                            Destination = reader["Destination"].ToString(),
                            DepartureTime = (DateTime)reader["DepartureTime"],
                            // ... more mapping
                        };

                        // FEATURE ENVY - calculating price here instead of in Flight/Service
                        var basePrice = (decimal)reader["BasePrice"];
                        var multiplier = 1.0m;

                        // DUPLICATED CODE - this logic appears in multiple places
                        var daysUntilDeparture = (flight.DepartureTime - DateTime.Now).TotalDays;
                        if (daysUntilDeparture > 30)
                        {
                            multiplier = 0.85m; // Early bird discount
                        }
                        else if (daysUntilDeparture < 3)
                        {
                            multiplier = 1.25m; // Last minute surcharge
                        }

                        if (passengers >= 5)
                        {
                            multiplier *= 0.9m; // Group discount
                        }

                        flight.BasePrice = new Money(basePrice * multiplier, "USD");

                        flights.Add(flight);
                    }
                }
            }

            // LONG METHOD continues...
            flights = flights.OrderBy(f => f.BasePrice.Amount).ToList();

            return View(flights);
        }
    }
    */
}
