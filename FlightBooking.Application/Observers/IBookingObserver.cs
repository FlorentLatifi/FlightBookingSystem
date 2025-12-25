using System.Threading.Tasks;

namespace FlightBooking.Application.Observers
{
    public interface IBookingObserver
    {
        string ObserverName { get; }
        Task NotifyAsync(BookingNotification notification);
    }
}
