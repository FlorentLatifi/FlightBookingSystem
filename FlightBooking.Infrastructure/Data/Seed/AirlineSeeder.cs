using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Seed
{
    public static class AirlineSeeder
    {
        public static Airline[] GetAirlines()
        {
            return new[]
            {
                new Airline { Id = 1, Code = "W6", Name = "Wizz Air", Country = "Hungary", LogoUrl = "" },
                new Airline { Id = 2, Code = "LH", Name = "Lufthansa", Country = "Germany", LogoUrl = "" },
                new Airline { Id = 3, Code = "OS", Name = "Austrian Airlines", Country = "Austria", LogoUrl = "" },
                new Airline { Id = 4, Code = "FR", Name = "Ryanair", Country = "Ireland", LogoUrl = "" },
                new Airline { Id = 5, Code = "BA", Name = "British Airways", Country = "United Kingdom", LogoUrl = "" },
                new Airline { Id = 6, Code = "AF", Name = "Air France", Country = "France", LogoUrl = "" },
                new Airline { Id = 7, Code = "TK", Name = "Turkish Airlines", Country = "Turkey", LogoUrl = "" },
                new Airline { Id = 8, Code = "LX", Name = "Swiss International Air Lines", Country = "Switzerland", LogoUrl = "" }
            };
        }
    }
}

