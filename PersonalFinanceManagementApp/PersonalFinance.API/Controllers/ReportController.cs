using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Reports;
using System.Security.Claims;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    public class ReportController :ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService) {
            _reportService = reportService;
            
        }



        [HttpGet("summary")]
        public async Task<IActionResult>GetSummary()
        {
            var userId = Guid.Parse(User.FindFirst( ClaimTypes.NameIdentifier)?.Value);

            var result =
                await _reportService
                    .GetSummaryAsync(userId);

            return Ok(
                ApiResponse<SummaryDto>
                    .SuccessResponse(
                        result,
                        "Summary retrieved"));
        }

        [HttpGet("monthly")]
        public async Task<IActionResult>GetMonthly(int month, int year)
        {
            var userId = Guid.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result =
                await _reportService.GetMonthlyReportAsync( userId,  month, year);

            return Ok(
                ApiResponse<MonthlyReportDto>
                    .SuccessResponse(
                        result,
                        "Summary retrieved"));
        }

        //Budget Usage
        [HttpGet("budget-usage")]
        public async Task<IActionResult> GetBudgetUsage()
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _reportService.GetBudgetUsageAsync(userId);

            return Ok(
                ApiResponse<List<BudgetUsageDto>>
                    .SuccessResponse(
                        result,
                        "Success"));
        }

        //EXpense category
        [HttpGet("expense-categories")]
        public async Task<IActionResult> GetExpenseCategories()
        {
            var userId = Guid.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result = await _reportService .GetExpenseByCategoryAsync(userId);

            return Ok(
                ApiResponse<List<ExpenseCategoryDto>>
                    .SuccessResponse(
                        result,
                        "Success"));
        }

        //Monthly Trend
        [HttpGet("monthly-trends")]
        public async Task<IActionResult> GetMonthlyTrends()
        {
            var userId = Guid.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

            var result =await _reportService.GetMonthlyTrendsAsync(userId);

            return Ok(
                ApiResponse<List<MonthlyTrendDto>>
                    .SuccessResponse(
                        result,
                        "Success"));
        }

    }
}
