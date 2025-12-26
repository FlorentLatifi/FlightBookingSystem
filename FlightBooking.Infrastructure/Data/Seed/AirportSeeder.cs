using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

nusing FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Seed
{
    public static class AirportSeeder
    {
        public static Airport[] GetAirports()
        {
            return new[]
            {
                new Airport { Id = 1, Code = "PRN", Name = "Prishtina International Airport", City = "Prishtina", Country = "Kosovo", Timezone = "Europe/Belgrade" },
                new Airport { Id = 2, Code = "VIE", Name = "Vienna International Airport", City = "Vienna", Country = "Austria", Timezone = "Europe/Vienna" },
                new Airport { Id = 3, Code = "MUC", Name = "Munich Airport", City = "Munich", Country = "Germany", Timezone = "Europe/Berlin" },
                new Airport { Id = 4, Code = "FCO", Name = "Leonardo da Vinci–Fiumicino Airport", City = "Rome", Country = "Italy", Timezone = "Europe/Rome" },
                new Airport { Id = 5, Code = "LHR", Name = "London Heathrow Airport", City = "London", Country = "United Kingdom", Timezone = "Europe/London" },
                new Airport { Id = 6, Code = "CDG", Name = "Charles de Gaulle Airport", City = "Paris", Country = "France", Timezone = "Europe/Paris" },
                new Airport { Id = 7, Code = "TIA", Name = "Tirana International Airport", City = "Tirana", Country = "Albania", Timezone = "Europe/Tirane" },
                new Airport { Id = 8, Code = "IST", Name = "Istanbul Airport", City = "Istanbul", Country = "Turkey", Timezone = "Europe/Istanbul" },
                new Airport { Id = 9, Code = "ATH", Name = "Athens International Airport", City = "Athens", Country = "Greece", Timezone = "Europe/Athens" },
                new Airport { Id = 10, Code = "ZRH", Name = "Zurich Airport", City = "Zurich", Country = "Switzerland", Timezone = "Europe/Zurich" }
            };
        }
    }
}

