using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Application.Interfaces.Services;
using FlightBooking.Application.Observers;
using FlightBooking.Application.Services;
using FlightBooking.Application.Strategies.Pricing;
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
builder.Services.AddScoped<FlightBooking.Application.Interfaces.Repositories.IFlightRepository, FlightRepository>();
builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<FlightBooking.Application.Interfaces.Repositories.IBookingRepository, BookingRepository>();
builder.Services.AddScoped<FlightBooking.Application.Interfaces.Repositories.ISeatRepository, SeatRepository>();
Console.WriteLine("✅ [Startup] Repositories u regjistruan!");

// ============================================
// 3. REGJISTRO INFRASTRUCTURE SERVICES
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar infrastructure services...");
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddScoped<ISmsService, SmsService>();
Console.WriteLine("✅ [Startup] Infrastructure services u regjistruan!");

// ============================================
// 4. REGJISTRO BUSINESS SERVICES
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar business services...");
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
// Repositories

builder.Services.AddScoped<IReservationRepository, ReservationRepository>();
builder.Services.AddScoped<IPassengerRepository, PassengerRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

// NOTIFICATION SERVICE ME OBSERVER PATTERN
builder.Services.AddScoped<INotificationService>(provider =>
{
    var emailService = provider.GetRequiredService<IEmailService>();
    var smsService = provider.GetRequiredService<ISmsService>();
    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

    var observers = new List<INotificationObserver>
    {
        new ReservationEmailObserver(emailService, loggerFactory.CreateLogger<ReservationEmailObserver>()),
        new ReservationSmsObserver(smsService, loggerFactory.CreateLogger<ReservationSmsObserver>())
    };

    return new NotificationService(emailService, observers, loggerFactory.CreateLogger<NotificationService>());
});
Console.WriteLine("✅ [Startup] NotificationService u regjistrua me 2 observers!");

// ============================================
// 5. REGJISTRO STRATEGY PATTERN (Pricing)
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar Pricing Strategy...");

// ZGJEDH STRATEGJINË (mund të bëhet edhe nga configuration)
var pricingStrategyConfig = builder.Configuration.GetValue<string>("PricingStrategy") ?? "Standard";

switch (pricingStrategyConfig.ToLower())
{
    case "discount":
        builder.Services.AddScoped<IPricingStrategy, DiscountPricingStrategy>();
        Console.WriteLine("✅ [Startup] DiscountPricingStrategy u aktivizua (10% OFF)!");
        break;
    case "seasonal":
        builder.Services.AddScoped<IPricingStrategy, SeasonalPricingStrategy>();
        Console.WriteLine("✅ [Startup] SeasonalPricingStrategy u aktivizua!");
        break;
    default:
        builder.Services.AddScoped<IPricingStrategy, StandardPricingStrategy>();
        Console.WriteLine("✅ [Startup] StandardPricingStrategy u aktivizua!");
        break;
}

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
// 7. INICIALIZO BAZËN E TË DHËNAVE
// ============================================
Console.WriteLine("\n🔧 [Database] Duke inicializuar bazën e të dhënave...");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // PËRDOR MIGRATIONS (jo EnsureCreated)
        Console.WriteLine("🔧 [Database] Duke aplikuar migrations...");
        context.Database.Migrate();

        Console.WriteLine("✅ [Database] Baza e të dhënave është gati!");
        Console.WriteLine($"📊 [Database] Connection: {context.Database.GetConnectionString()}");
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "❌ [Database] ERROR gjatë inicializimit të bazës!");
        Console.WriteLine($"❌ [Database] ERROR: {ex.Message}");
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
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

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
Console.WriteLine("    ├── Domain Layer (Entities + Enums + Value Objects)");
Console.WriteLine("    ├── Application Layer (Services + DTOs + Patterns)");
Console.WriteLine("    ├── Infrastructure Layer (DbContext + Repositories)");
Console.WriteLine("    └── Presentation Layer (MVC Controllers + Views)");
Console.WriteLine("");
Console.WriteLine("🎯 Design Patterns:");
Console.WriteLine("    ├── MVC Pattern");
Console.WriteLine("    ├── Repository Pattern");
Console.WriteLine($"    ├── Strategy Pattern (Active: {pricingStrategyConfig})");
Console.WriteLine("    ├── Observer Pattern (Email + SMS)");
Console.WriteLine("    └── Dependency Injection");
Console.WriteLine("");
Console.WriteLine("💾 Database: SQL Server with Migrations");
Console.WriteLine("🌐 Framework: ASP.NET Core 8.0 MVC");
Console.WriteLine("============================================");
Console.WriteLine("🚀 Aplikacioni po fillon...");
Console.WriteLine("============================================\n");

app.Run();