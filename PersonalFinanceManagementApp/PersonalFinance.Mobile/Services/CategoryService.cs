using PersonalFinance.Shared.DTOs.Categories;
using PersonalFinance.Shared.DTOs.Common;
namespace PersonalFinance.Mobile.Services
{
    public class CategoryService
    {

        //Call API backend
        private readonly ApiService _apiService;

        public CategoryService()
        {
            _apiService = new ApiService();
        }

        //-----------------------Get all categories
        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            //Call backend;
            //Get/api/categories
            var response = await _apiService
                .GetAsync<ApiResponse<List<CategoryDto>>>("categories");

            //Validate result
            if(response.Data == null) return new List<CategoryDto>();


            return response.Data;   
        }

        //-------------------CREATE-----------------
        public async Task CreateCategoryAsync(CreateCategoryRequestDto request)
        {
            await  _apiService.PostAsync<object>("categories",request);
        }

        //------------UPDATE -----------
        public async Task UpdateCategoryAsync(Guid id, UpdateCategoryRequestDto request)
        {
            await _apiService.PutAsync($"categories/{id}", request);
        }

        //---------------------DELETE 
        public async Task DeleteCategoryAsync(Guid id)
        {
            await _apiService.DeleteAsync($"categories/{id}");
        }
    }
}
