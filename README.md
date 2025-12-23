Flight Booking System - Readme
✈️ Flight Booking System
📌 Përshkrimi i Projektit

Flight Booking System është një aplikacion web akademik i ndërtuar me ASP.NET Core MVC, i cili simulon një sistem real për kërkimin, rezervimin dhe pagesën e fluturimeve. Projekti është ndërtuar duke respektuar Onion Architecture / Clean Architecture dhe demonstron përdorimin praktik të Design Patterns të avancuara.

Ky projekt është zhvilluar për qëllime akademike, por me logjikë reale biznesi dhe strukturë profesionale, të ngjashme me aplikacionet enterprise.

🎯 Qëllimi i Projektit

Demonstrimi i arkitekturës së pastër (Clean / Onion Architecture)

Zbatimi praktik i Design Patterns (Strategy, Observer, Repository, MVC)

Ndarja e qartë e përgjegjësive midis shtresave

Simulimi i një sistemi real të rezervimit të fluturimeve

🧱 Arkitektura e Projektit

Projekti ndjek Onion Architecture, e ndarë në këto shtresa:

FlightBooking
│
├── FlightBooking.Domain → Entitete, Enums, Business Rules
├── FlightBooking.Application → Business Logic, Services, Interfaces
├── FlightBooking.Infrastructure→ Database, Repositories, External Services
├── FlightBooking.Web → MVC Controllers, Views, UI
🔹 Domain Layer

Entitete: Flight, Reservation, Passenger, Payment

Enums: SeatClass, ReservationStatus, PaymentStatus

Business rules (p.sh. CanBeBooked(), CanBeCancelled())

🔹 Application Layer

Interfaces për Services dhe Repositories

Business Services:

FlightService

ReservationService

PaymentService

NotificationService

Implementim i Strategy Pattern për çmime

Implementim i Observer Pattern për njoftime

🔹 Infrastructure Layer

ApplicationDbContext (Entity Framework Core)

Repository implementations (EF Core)

EmailService (mock)

Seed data për testim

🔹 Web Layer (MVC)

Controllers: HomeController, ReservationController

Razor Views (UI)

Bootstrap 5 për dizajn

🧠 Design Patterns të Përdorura
✅ MVC Pattern

Ndarje e qartë: Controller → Service → Repository → Database

✅ Repository Pattern

Abstraktim i aksesit në të dhëna

Lehtësi për testim dhe mirëmbajtje

✅ Strategy Pattern (Pricing)

IPricingStrategy

Implementime:

StandardPricingStrategy

DiscountPricingStrategy

SeasonalPricingStrategy

📌 Strategjia zgjidhet në Program.cs pa ndryshuar kodin ekzistues.

✅ Observer Pattern (Notifications)

NotificationSubject

Observers:

EmailNotificationObserver

SmsNotificationObserver

📌 Njoftime paralele për konfirmime, anulime dhe pagesa.

⚙️ Teknologjitë e Përdorura

ASP.NET Core MVC (.NET 8)

Entity Framework Core

SQL Server LocalDB

Razor Views

Bootstrap 5

Dependency Injection (built-in)

💾 Database

SQL Server LocalDB

Database krijohet automatikisht në startup:

context.Database.EnsureCreated();

Connection string ruhet në appsettings.json

🔧 Konfigurimi & Ekzekutimi
1️⃣ Kërkesat

Visual Studio 2022+

.NET SDK 8.0

SQL Server LocalDB

2️⃣ Clone Repository
git clone https://github.com/your-username/flight-booking-system.git
3️⃣ Build & Run
dotnet build
dotnet run

Ose:

Ctrl + Shift + B

F5 në Visual Studio

🧪 Funksionalitetet Kryesore

🔍 Kërkim fluturimesh sipas destinacionit dhe datës

🪑 Kontroll i disponibilitetit të ulëseve

🧾 Krijim rezervimi me kod unik

💳 Pagesë (simulim payment gateway)

❌ Anulim rezervimi + rimburs

📧📱 Njoftime Email & SMS (mock)

📸 UI – Flow i Përdoruesit

Home Page – Search Flights

Shfaqja e rezultateve

Zgjedhja e fluturimit

Rezervimi + Pagesa

Konfirmimi ose dështimi

🎓 Vlera Akademike

Arkitekturë enterprise

Design Patterns të zbatuara realisht

Kod i pastër dhe i strukturuar

Gati për prezantim, provim dhe mbrojtje

👤 Autori

Florent Latifi
Student – Shkenca Kompjuterike / Inxhinieri Softuerike
