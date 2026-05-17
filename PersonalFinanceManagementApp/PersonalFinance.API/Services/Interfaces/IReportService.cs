using PersonalFinance.Shared.DTOs.Reports;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface IReportService
    {
        Task<List<BudgetUsageDto>>GetBudgetUsageAsync(Guid userId);

        Task<SummaryDto> GetSummaryAsync(Guid userId);

        Task<MonthlyReportDto> GetMonthlyReportAsync(Guid userId, int month,int year);
    }
}
