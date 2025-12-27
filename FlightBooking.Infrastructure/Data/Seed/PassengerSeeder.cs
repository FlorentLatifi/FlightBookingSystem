using Microsoft.EntityFrameworkCore;
using FlightBooking.Domain.Entities;

namespace FlightBooking.Infrastructure.Data.Seed
{
    /// <summary>
    /// Seeder për Passenger data (demo purposes)
    /// OPSIONAL: Mund të mos thirret në ApplicationDbContext
    /// </summary>
    public static class PassengerSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasData(
                new Passenger
                {
                    Id = 1,
                    FirstName = "Florent",
                    LastName = "Latifi",
                    Email = "florent.latifi@example.com",
                    PhoneNumber = "+38349123456",
                    PassportNumber = "KS123456",
                    DateOfBirth = new DateTime(1995, 5, 15),
                    Nationality = "Kosovar"
                },
                new Passenger
                {
                    Id = 2,
                    FirstName = "Arben",
                    LastName = "Krasniqi",
                    Email = "arben.krasniqi@example.com",
                    PhoneNumber = "+38349234567",
                    PassportNumber = "KS234567",
                    DateOfBirth = new DateTime(1992, 8, 20),
                    Nationality = "Kosovar"
                },
                new Passenger
                {
                    Id = 3,
                    FirstName = "Blerta",
                    LastName = "Berisha",
                    Email = "blerta.berisha@example.com",
                    PhoneNumber = "+38349345678",
                    PassportNumber = "KS345678",
                    DateOfBirth = new DateTime(1998, 3, 10),
                    Nationality = "Kosovar"
                }
            );
        }
    }
}