using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Observers;
using FlightBooking.Application.Services;
using FlightBooking.Application.Strategies;
using FlightBooking.Infrastructure.Data;
using FlightBooking.Infrastructure.Repositories;
using FlightBooking.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ============================================
// 1. KONFIGURO DATABASE (DbContext)
// ============================================
Console.WriteLine("🔧 [Startup] Duke konfiguruar bazën e të dhënave...");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.MigrationsAssembly("FlightBooking.Infrastructure")
    )
);
Console.WriteLine("✅ [Startup] Baza e të dhënave u konfigurua!");

// ============================================
// 2. REGJISTRO REPOSITORIES (Data Access Layer)
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar repositories...");
builder.Services.AddScoped<IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
Console.WriteLine("✅ [Startup] Repositories u regjistruan!");

// ============================================
// 3. REGJISTRO INFRASTRUCTURE SERVICES
// (Duhet para Business Services sepse EmailService nevojitet për NotificationService)
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar infrastructure services...");
builder.Services.AddScoped<IEmailService, EmailService>();
Console.WriteLine("✅ [Startup] Infrastructure services u regjistruan!");

// ============================================
// 4. REGJISTRO BUSINESS SERVICES
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar business services...");
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();

// ===== NOTIFICATION SERVICE ME OBSERVER PATTERN =====
// Observers krijohen DIREKT brenda NotificationService
builder.Services.AddScoped<INotificationService>(provider =>
{
    // Merr EmailService nga DI (duhet për EmailNotificationObserver)
    var emailService = provider.GetRequiredService<IEmailService>();

    // Krijo observers DIREKT (pa i regjistruar më parë në DI)
    var observers = new List<INotificationObserver>
    {
        new EmailNotificationObserver(emailService),  // ✅ Email notification
        new SmsNotificationObserver()                 // ✅ SMS notification (mock)
    };

    // Kthen NotificationService me observers të inicializuara
    // ✅ SAKTË - Kalojmë të dy parametrat
    return new NotificationService(emailService, observers);
});
Console.WriteLine("✅ [Startup] NotificationService u regjistrua me 2 observers (Email + SMS)!");

// ============================================
// 5. REGJISTRO STRATEGY PATTERN
// Duke zgjedhur cilën strategji të përdorim për llogaritjen e çmimeve
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar Pricing Strategy...");

// ⚠️ ZGJEDH NJË NGA KËTO TRI STRATEGJI:

// OPSIONI 1: Standard Pricing (çmimet normale)
builder.Services.AddScoped<IPricingStrategy, StandardPricingStrategy>();
Console.WriteLine("✅ [Startup] StandardPricingStrategy u aktivizua!");

// OPSIONI 2: Discount Pricing (10% zbritje) - Komento opsionin 1 dhe aktivizo këtë
// builder.Services.AddScoped<IPricingStrategy, DiscountPricingStrategy>();
// Console.WriteLine("✅ [Startup] DiscountPricingStrategy u aktivizua (10% OFF)!");

// OPSIONI 3: Seasonal Pricing (çmime sipas sezonit) - Komento opsionin 1 dhe aktivizo këtë
// builder.Services.AddScoped<IPricingStrategy, SeasonalPricingStrategy>();
// Console.WriteLine("✅ [Startup] SeasonalPricingStrategy u aktivizua!");

// 📝 DEMONSTRIM: Ndryshimi i strategjisë është SHUMË I THJESHTË!
// Thjesht komento një rresht dhe aktivizo tjetrin. Gjithçka tjetër mbetet e njëjtë!
// Kjo është fuqia e Strategy Pattern!

Console.WriteLine("✅ [Startup] Business services u regjistruan!");

// ============================================
// 6. REGJISTRO MVC CONTROLLERS & VIEWS
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar MVC...");
builder.Services.AddControllersWithViews();
Console.WriteLine("✅ [Startup] MVC u konfigurua!");

// ============================================
// BUILD APPLICATION
// ============================================
var app = builder.Build();

// ============================================
// 7. KRIJO BAZËN E TË DHËNAVE (nëse nuk ekziston)
// ============================================
Console.WriteLine("\n🔧 [Database] Duke kontrolluar bazën e të dhënave...");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Krijo bazën nëse nuk ekziston
        Console.WriteLine("🔧 [Database] Duke krijuar bazën (EnsureCreated)...");
        context.Database.EnsureCreated();

        Console.WriteLine("✅ [Database] Baza e të dhënave është gati!");
        Console.WriteLine($"📊 [Database] Connection: {context.Database.GetConnectionString()}");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ [Database] ERROR: {ex.Message}");
        Console.WriteLine($"📄 [Database] StackTrace: {ex.StackTrace}");
    }
}

// ============================================
// 8. MIDDLEWARE PIPELINE CONFIGURATION
// ============================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Në development mode, shfaq exceptions të detajuara
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Default route: Home/Index
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// ============================================
// 9. PRINT SUMMARY & START
// ============================================
Console.WriteLine("\n============================================");
Console.WriteLine("✅ FLIGHT BOOKING SYSTEM - GATI!");
Console.WriteLine("============================================");
Console.WriteLine("📦 Architecture: Onion Architecture");
Console.WriteLine("    ├── Domain Layer (Entities + Enums)");
Console.WriteLine("    ├── Application Layer (Services + DTOs + Patterns)");
Console.WriteLine("    ├── Infrastructure Layer (DbContext + Repositories)");
Console.WriteLine("    └── Presentation Layer (MVC Controllers + Views)");
Console.WriteLine("");
Console.WriteLine("🎯 Design Patterns:");
Console.WriteLine("    ├── MVC Pattern (Controllers + Views + Models)");
Console.WriteLine("    ├── Repository Pattern (Data Access Abstraction)");
Console.WriteLine("    ├── Strategy Pattern (Dynamic Pricing: Standard/Discount/Seasonal)");
Console.WriteLine("    ├── Observer Pattern (Parallel Notifications: Email + SMS)");
Console.WriteLine("    └── Dependency Injection (DI Container)");
Console.WriteLine("");
Console.WriteLine("💾 Database: SQL Server LocalDB");
Console.WriteLine("🔧 DI: Microsoft.Extensions.DependencyInjection");
Console.WriteLine("🌐 Framework: ASP.NET Core 8.0 MVC");
Console.WriteLine("============================================");
Console.WriteLine("🚀 Aplikacioni po fillon...");
Console.WriteLine("🌍 URL: https://localhost:XXXX");
Console.WriteLine("============================================\n");

app.Run();