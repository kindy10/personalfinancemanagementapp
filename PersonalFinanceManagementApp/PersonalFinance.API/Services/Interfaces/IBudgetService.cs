using PersonalFinance.Shared.DTOs.Budgets;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface IBudgetService
    {
        Task<List<BudgetDto>> GetAllAsync(Guid userId);
        Task<BudgetDto> CreateAsync(Guid userId, CreateBudgetRequestDto request);
    }
}
