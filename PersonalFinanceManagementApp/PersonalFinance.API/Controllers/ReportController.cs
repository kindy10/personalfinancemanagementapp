using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Enums;
using PersonalFinance.Shared.DTOs.Reports;
using System.Security.Claims;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController :ControllerBase
    {
        private readonly AppDbContext _context;

        public ReportController(AppDbContext context) { _context = context; }



        [HttpGet("summary")]
        public async Task<IActionResult> GetSummary()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var income = await _context.Transactions
                .Where(t => t.UserId == userId && t.Category.Type == CategoryType.Income)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            var expense = await _context.Transactions
                .Where(t => t.UserId == userId && t.Category.Type == CategoryType.Expense)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            var result = new SummaryDto
            {
                TotalIncome = income,
                TotalExpense = expense,
                Balance = income - expense
            };
            return Ok(ApiResponse<SummaryDto>.SuccessResponse(result, "Summary retrieved"));

        }

        [HttpGet("monthly")]
        public async Task<IActionResult> GetMonthly(int month,int year)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var income = await _context.Transactions
                .Where(t=> t.UserId == userId && 
                    t.Date.Month == month &&
                    t.Date.Year == year &&
                    t.Category.Type == CategoryType.Income)
                .SumAsync(t=> (decimal?)t.Amount) ?? 0;

            var expense = await _context.Transactions
                .Where(t => t.UserId == userId &&
                    t.Date.Month == month &&
                    t.Date.Year == year &&
                    t.Category.Type == CategoryType.Expense)
                .SumAsync(t => (decimal?)t.Amount) ?? 0;

            var result = new MonthlyReportDto
            {
                Month = month,
                Year = year,
                TotalIncome = income,
                TotalExpenses = expense,
                 Balance = income - expense

            };
            return Ok(ApiResponse<MonthlyReportDto>.SuccessResponse(result, "Summary retrieved"));

        }


    }
}
