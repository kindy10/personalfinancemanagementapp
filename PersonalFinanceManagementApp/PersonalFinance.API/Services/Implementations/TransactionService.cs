using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Exceptions;
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

        //--------------------------CREATE ----------------------------///
        public async Task<TransactionDto> CreateAsync(Guid userId, CreateTransactionRequestDto request)
        {
            //Check Amount  constraint
            if (request.Amount <= 0)
                throw new AppException("Amount must be greater than zero");

            //check category
            if (request.CategoryId == Guid.Empty)
                throw new AppException("Category is required");

            //check description
            if (string.IsNullOrWhiteSpace(request.Description))
                throw new AppException("Description is required");
            //Check category
            var categoryExists = await _context.Categories
                .AnyAsync(c => c.Id == request.CategoryId && c.UserId == userId);
            if (!categoryExists)
                throw new AppException("Invalid category");
            
            //Create the transaction
            var transaction = new Transaction
            {
                Id = Guid.NewGuid(),
                Amount = request.Amount,
                Date = DateTime.UtcNow,  //real life event  ( when the transaction actually happened
                Description = request.Description,
                CategoryId = request.CategoryId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow, // when the record was created in the system
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

       //------------------------------------DELETE----------------------------//
        public async Task  DeleteAsync(Guid id, Guid userId)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
            if (transaction == null) 
                throw new AppException("Transaction not found");

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();
           
        }

        //----------------------------------UPDATE--------------------------------//
        public async Task<TransactionDto> UpdateAsync(Guid id, Guid userId, UpdateTransactionRequestDto request)
            {
                var transaction = await _context.Transactions
                    .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

                if (transaction is null)
                    throw new AppException("Transaction not found");

                if (request.Amount <= 0)
                    throw new AppException("Amount must be greater than zero");

                transaction.Amount = request.Amount;
                transaction.Date = request.Date;
                transaction.Description = request.Description;
                transaction.CategoryId = request.CategoryId;

                await _context.SaveChangesAsync();

                return new TransactionDto
                {
                    Id = transaction.Id,
                    Amount = transaction.Amount,
                    Description = transaction.Description,
                    Date = transaction.Date,
                    CategoryId = transaction.CategoryId,
                }; 
        }
    }
        
}