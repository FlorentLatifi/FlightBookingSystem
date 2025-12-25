using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightBooking.Domain.Enums
{
    public enum FlightStatus
    {
        Scheduled = 1,
        Boarding = 2,
        Departed = 3,
        Arrived = 4,
        Delayed = 5,
        Cancelled = 6
    }
}