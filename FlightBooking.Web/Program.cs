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
// 3. REGJISTRO BUSINESS SERVICES
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar business services...");
builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<INotificationService, NotificationService>();
Console.WriteLine("✅ [Startup] Business services u regjistruan!");

// ============================================
// 4. REGJISTRO INFRASTRUCTURE SERVICES
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar infrastructure services...");
builder.Services.AddScoped<IEmailService, EmailService>();
Console.WriteLine("✅ [Startup] Infrastructure services u regjistruan!");

// ============================================
// 5. REGJISTRO STRATEGY PATTERN
// Duke zgjedhur cilën strategji të përdorim
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

// ============================================
// 6. REGJISTRO OBSERVER PATTERN
// Të gjithë observers që duam të njoftojmë
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar Observers...");

// Regjistro çdo observer individualisht
builder.Services.AddScoped<INotificationObserver, EmailNotificationObserver>();
builder.Services.AddScoped<INotificationObserver, SmsNotificationObserver>();

// Regjistro collection të të gjithë observers
builder.Services.AddScoped<IEnumerable<INotificationObserver>>(provider =>
{
    return new List<INotificationObserver>
    {
        provider.GetRequiredService<EmailNotificationObserver>(),
        provider.GetRequiredService<SmsNotificationObserver>()
    };
});

Console.WriteLine("✅ [Startup] 2 Observers u regjistruan (Email + SMS)!");

// 📝 DEMONSTRIM: Lehtë të shtosh observers të rinj!
// Thjesht shto një rresht tjetër për observer të ri (p.sh. PushNotificationObserver)

// ============================================
// 7. REGJISTRO MVC CONTROLLERS & VIEWS
// ============================================
Console.WriteLine("🔧 [Startup] Duke regjistruar MVC...");
builder.Services.AddControllersWithViews();
Console.WriteLine("✅ [Startup] MVC u konfigurua!");

// ============================================
// BUILD APPLICATION
// ============================================
var app = builder.Build();

// ============================================
// 8. KRIJO BAZËN E TË DHËNAVE (nëse nuk ekziston)
// ============================================
Console.WriteLine("\n🔧 [Startup] Duke kontrolluar bazën e të dhënave...");
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();

        // Krijo bazën nëse nuk ekziston
        context.Database.EnsureCreated();

        Console.WriteLine("✅ [Startup] Baza e të dhënave është gati!");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ [Startup] ERROR gjatë krijimit të bazës: {ex.Message}");
    }
}

// ============================================
// 9. MIDDLEWARE PIPELINE CONFIGURATION
// ============================================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
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
// 10. PRINT SUMMARY & START
// ============================================
Console.WriteLine("\n============================================");
Console.WriteLine("✅ FLIGHT BOOKING SYSTEM - GATI!");
Console.WriteLine("============================================");
Console.WriteLine("📦 Architecture: Onion Architecture");
Console.WriteLine("🎯 Patterns: MVC + Repository + Strategy + Observer");
Console.WriteLine("💾 Database: SQL Server LocalDB");
Console.WriteLine("🔧 DI Container: Microsoft.Extensions.DependencyInjection");
Console.WriteLine("============================================");
Console.WriteLine("🚀 Aplikacioni po fillon...\n");

app.Run();