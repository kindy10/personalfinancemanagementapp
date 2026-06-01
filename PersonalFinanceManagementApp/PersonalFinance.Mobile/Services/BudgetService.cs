using PersonalFinance.Shared.DTOs.Budgets;
using PersonalFinance.Shared.DTOs.Common;

namespace PersonalFinance.Mobile.Services
{
    public  class BudgetService
    {
        private readonly ApiService _apiService;

        public BudgetService()
        {
            _apiService = new ApiService();
        }

        //-----------GET BUDGET
        public async Task<List<BudgetDto>> GetBudgetsAsync()
        {
            var response = await _apiService.GetAsync<ApiResponse<List<BudgetDto>>>("budgets");

            if(response.Data ==null)
                return new List<BudgetDto>();

            return response.Data;

        }

        //CREATE budget
        public async Task CreateBudgetAsync(CreateBudgetRequestDto request)
        {
            await _apiService.PostAsync<object>("budgets", request);
        }

        //-----------UPDATA BUDGET
        public async Task UpdateBudgetAsync(Guid id,UpdateBudgetRequestDto request)
        {

            await _apiService.PutAsync($"budgets/{id}", request);
        }

        //Delete budget
        public async Task DeleteBudgetAsync(Guid id)
        {
            await _apiService.DeleteAsync($"budgets/{id}");
        }
    }
}
