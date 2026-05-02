using PersonalFinance.API.Data;
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

        public async Task<List<BudgetDto>> GetAllAsync(Guid userId)
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
        public async Task<BudgetDto> CreateAsync(Guid userId,CreateBudgetRequestDto request)
        {
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
    }
}
