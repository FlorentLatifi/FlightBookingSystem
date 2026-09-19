# Flight booking system

[Shqip](README.sq.md)

An ASP.NET Core MVC application for searching, reserving and paying for flights. I built it solo as a university project to practise Onion (Clean) architecture and classic design patterns on a realistic domain.

## Features

- Search flights by destination and date
- Seat availability checks
- Reservations with a unique booking code
- Payment through a simulated gateway, with cancellation and refunds
- Email and SMS notifications (mocked)
- Admin pages for flights and passengers

## Architecture

The solution follows Onion architecture. Dependencies point inward, so the domain knows nothing about the database or the web.

```
FlightBooking.Web             MVC controllers, Razor views, DI setup in Program.cs
FlightBooking.Infrastructure  EF Core DbContext, repositories, migrations, seed data, mock email/SMS
FlightBooking.Application     services, commands, pricing strategies, notification observers, DTOs
FlightBooking.Domain          entities, enums, value objects, business rules
```

- **Entities:** Flight, Reservation, Passenger, Payment, Booking, Seat
- **Value objects:** `Money` (amount and currency; adding different currencies throws) and `SeatNumber`
- **Business rules on the entities:** `CanBeBooked`, `CanBeCancelled`, `CanBeRefunded`

## Design patterns

| Pattern | Where | What it does |
|---|---|---|
| Repository | `Application/Interfaces/Repositories`, implemented in Infrastructure | Keeps EF Core out of the application layer |
| Strategy | `Application/Strategies` | Standard, early-bird, last-minute, group, discount and seasonal pricing behind `IPricingStrategy` |
| Observer | `Application/Observers` | Email, SMS and database-log observers, notified in parallel with `Task.WhenAll` |
| Value object | `Domain/ValueObjects` | Replaces raw decimals and strings with validated types |
| MVC | `FlightBooking.Web` | Controller → service → repository |

The default pricing strategy comes from the `PricingStrategy` setting in `appsettings.json` (`Standard`, `Discount` or `Seasonal`), and `PricingService` can switch strategies at runtime.

### Parallel payment processing

`ProcessPaymentCommand` runs the payment and the notification preparation at the same time with `Task.WhenAll`. The reservation is confirmed and notifications are sent only if the payment succeeds.

## Tech stack

.NET 8, ASP.NET Core MVC, Razor views, Bootstrap 5, Entity Framework Core 8 with migrations, SQL Server LocalDB, built-in dependency injection and logging.

## Run it

Requires the .NET 8 SDK and SQL Server LocalDB (installed with Visual Studio).

```bash
git clone https://github.com/FlorentLatifi/FlightBookingSystem.git
cd FlightBookingSystem
dotnet run --project FlightBooking.Web
```

Migrations run on startup and seed sample airports, airlines and flights. The app's URL is printed in the console.

## More documentation

These are in Albanian:

- `REFACTORING_REPORT.md`: refactoring techniques applied, with before/after examples
- `architecture.md.txt`: the layers in detail
- `design-pattern.md.txt`: each pattern with code
- `setup.md.txt`: setup guide

## Author

Florent Latifi, BSc Computer Science and Engineering, UBT
