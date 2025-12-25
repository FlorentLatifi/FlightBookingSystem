using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using FlightBooking.Application.Interfaces.Repositories;
using FlightBooking.Domain.Entities;
using FlightBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FlightBooking.Infrastructure.Repositories
{
    /// <summary>
    /// Implementimi i IPaymentRepository
    /// </summary>
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Payment>> GetAllAsync()
        {
            return await _context.Payments
                .Include(p => p.Reservation!)
                    .ThenInclude(r => r.Flight)
                .Include(p => p.Reservation!)
                    .ThenInclude(r => r.Passenger)
                .OrderByDescending(p => p.PaymentDate)
                .ToListAsync();
        }

        public async Task<Payment?> GetByIdAsync(int id)
        {
            return await _context.Payments
                .Include(p => p.Reservation!)
                    .ThenInclude(r => r.Flight)
                .Include(p => p.Reservation!)
                    .ThenInclude(r => r.Passenger)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Payment?> GetByReservationIdAsync(int reservationId)
        {
            return await _context.Payments
                .Include(p => p.Reservation!)
                    .ThenInclude(r => r.Flight)
                .Include(p => p.Reservation!)
                    .ThenInclude(r => r.Passenger)
                .FirstOrDefaultAsync(p => p.ReservationId == reservationId);
        }

        public async Task<Payment?> GetByTransactionIdAsync(string transactionId)
        {
            return await _context.Payments
                .Include(p => p.Reservation)
                .FirstOrDefaultAsync(p => p.TransactionId == transactionId);
        }

        public async Task AddAsync(Payment payment)
        {
            await _context.Payments.AddAsync(payment);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Payment payment)
        {
            _context.Payments.Update(payment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await GetByIdAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
                await _context.SaveChangesAsync();
            }
        }
    }
}
