using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Models;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Transactions;

namespace PersonalFinance.API.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly AppDbContext _context;

        public TransactionService(AppDbContext context)
        {
            _context = context;
        }

        public  async Task<List<TransactionDto>> GetAllAsync(Guid userId)
        { 
            return  _context.Transactions    // Must  Controle for await  
                .Where(t => t.UserId == userId)
                .Select(t => new TransactionDto
                {
                    Id = t.Id,
                    Amount = t.Amount,
                    Date = t.Date,
                    Description = t.Description,
                    CategoryId = t.CategoryId,
                    CategoryName = t.Category.Name

                })
                .ToList();
        }

        public async Task<TransactionDto> CreateAsync(Guid userId, CreateTransactionRequestDto request)
        {

            //Create the transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Date = request.Date,
                Description = request.Description,
                CategoryId = request.CategoryId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };
            //Add it to the database
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return new TransactionDto
            {
                Id = transaction.Id,
                Amount = transaction.Amount,
                Date = transaction.Date,
                Description = transaction.Description,
                CategoryId = transaction.CategoryId,
            };
            

        }

        public async Task<bool> DeleteAsync(Guid id, Guid userId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
