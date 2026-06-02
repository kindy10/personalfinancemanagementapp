using PersonalFinance.API.Models;
using PersonalFinance.Shared.DTOs.Reports;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<BudgetUsageDto>>GetBudgetUsageAsync(Guid userId);
        Task<List<BudgetUsageDto>> GetAllBudgetUsageAsync(Guid userId);

        Task<SummaryDto> GetSummaryAsync(Guid userId);

        Task<MonthlyReportDto> GetMonthlyReportAsync(Guid userId, int month,int year);
        Task<List<ExpenseCategoryDto>>GetExpenseByCategoryAsync(Guid userId);
        Task<List<MonthlyTrendDto>> GetMonthlyTrendsAsync(Guid userId);
    }
}
