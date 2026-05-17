using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Enums;
using PersonalFinance.Shared.DTOs.Reports;

namespace PersonalFinance.API.Services.Implementations
{
    public class ReportService:IReportService
    {
        private readonly AppDbContext _context;

        public ReportService(AppDbContext context) { 
            _context = context; 

        }

        public async Task<List<BudgetUsageDto>>GetBudgetUsageAsync(Guid userId)
        {
            var currentMonth =
                new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    1);

            // Get current month budgets
            var budgets =
                await _context.Budgets
                    .Include(b => b.Category)
                    .Where(b =>
                        b.UserId == userId &&
                        b.Month.Year == currentMonth.Year &&
                        b.Month.Month == currentMonth.Month)
                    .ToListAsync();

            var result =
                new List<BudgetUsageDto>();

            foreach (var budget in budgets)
            {
                // Calculate spent amount
                var spent =
                    await _context.Transactions
                        .Where(t =>
                            t.UserId == userId &&
                            t.CategoryId == budget.CategoryId &&
                            t.Date.Month == currentMonth.Month &&
                            t.Date.Year == currentMonth.Year)
                        .SumAsync(t =>
                            (decimal?)t.Amount) ?? 0;

                result.Add(new BudgetUsageDto
                {
                    CategoryName =
                        budget.Category.Name,

                    Limit =
                        budget.MonthlyLimit,

                    Spent =
                        spent,

                    Remaining =
                        budget.MonthlyLimit - spent,

                    PercentageUsed =
                        budget.MonthlyLimit == 0
                            ? 0
                            : (double)(spent / budget.MonthlyLimit )
                });
            }

            return result;
        }

        //------------Summary
        public async Task<SummaryDto>GetSummaryAsync(Guid userId)
        {
            var income =
                await _context.Transactions
                    .Where(t =>
                        t.UserId == userId &&
                        t.Category.Type ==
                            CategoryType.Income)
                    .SumAsync(t =>
                        (decimal?)t.Amount) ?? 0;

            var expense =
                await _context.Transactions
                    .Where(t =>
                        t.UserId == userId &&
                        t.Category.Type ==
                            CategoryType.Expense)
                    .SumAsync(t =>
                        (decimal?)t.Amount) ?? 0;

            return new SummaryDto
            {
                TotalIncome = income,

                TotalExpense = expense,

                Balance = income - expense
            };
        }


        //-----------------Monthly report
        public async Task<MonthlyReportDto>GetMonthlyReportAsync(Guid userId,int month,int year)
        {
            var income =
                await _context.Transactions
                    .Where(t =>
                        t.UserId == userId &&
                        t.Date.Month == month &&
                        t.Date.Year == year &&
                        t.Category.Type ==
                            CategoryType.Income)
                    .SumAsync(t =>
                        (decimal?)t.Amount) ?? 0;

            var expense =
                await _context.Transactions
                    .Where(t =>
                        t.UserId == userId &&
                        t.Date.Month == month &&
                        t.Date.Year == year &&
                        t.Category.Type ==
                            CategoryType.Expense)
                    .SumAsync(t =>
                        (decimal?)t.Amount) ?? 0;

            return new MonthlyReportDto
            {
                Month = month,

                Year = year,

                TotalIncome = income,

                TotalExpenses = expense,

                Balance = income - expense
            };
        }
    }
}
