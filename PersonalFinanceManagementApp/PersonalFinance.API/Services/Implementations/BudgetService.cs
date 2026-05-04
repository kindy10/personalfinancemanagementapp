using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Exceptions;
using PersonalFinance.API.Models;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Budgets;

namespace PersonalFinance.API.Services.Implementations
{
    public class BudgetService : IBudgetService
    {
        private readonly AppDbContext _context;

        public BudgetService(AppDbContext context) {
            _context = context;
        }

        public  async Task<List<BudgetDto>>  GetAllAsync(Guid userId)
        {
            return _context.Budgets
                .Where(b=> b.UserId == userId)
                .Select(b => new BudgetDto
                {
                    Id = b.Id,
                    MonthlyLimit = b.MonthlyLimit,
                    Month = b.Month,
                    Year = b.Year,
                    CategoryId = b.CategoryId,
                    CategoryName = b.Category.Name,
                })
                .ToList();
        }

        //----------------------------CREATE --------------------------//
        public async Task<BudgetDto> CreateAsync(Guid userId,CreateBudgetRequestDto request)
        {
            //Check Month
            if (request.Month < 1 || request.Month > 12)
                throw new AppException("Invalid month");

            //Check Year
            if (request.Year < 2000)
                throw new AppException("Invalid year");
            var budget = new Budget
            {
                Id = Guid.NewGuid(),
                MonthlyLimit = request.MonthlyLimit,
                Month = request.Month,
                Year = request.Year,
                CategoryId = request.CategoryId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow
            };

            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();

            return new BudgetDto
            {
                Id = budget.Id,
                MonthlyLimit = budget.MonthlyLimit,
                Month = budget.Month,
                Year = budget.Year,
                CategoryId = budget.CategoryId,
            };
        }

        //------------------------------UPDATE---------------------------------------------//
        public async Task<BudgetDto> UpdateAsync(Guid id, Guid userId, UpdateBudgetRequestDto request)
        {
            var budget =await  _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (budget is null)
                throw new AppException("Budget not found ");

            budget.MonthlyLimit = request.MonthlyLimit;
            budget.Month = request.Month;
            budget.Year = request.Year;
            budget.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();
            return new BudgetDto { 
                Id = budget.Id,
                MonthlyLimit = budget.MonthlyLimit,
                Year= budget.Year,
                CategoryId = budget.CategoryId,
            };
        }

        //-------------------------DELETE -----------------------------------------------------//
        public async Task DeleteAsync(Guid id, Guid userId)
        {
            var budget = await _context.Budgets
                .FirstOrDefaultAsync(b => b.Id == id && b.UserId == userId);
            if (budget is null)
                throw new AppException("Budget not found");

            _context.Budgets .Remove(budget);
            await _context.SaveChangesAsync();
           
        }
    }
}
