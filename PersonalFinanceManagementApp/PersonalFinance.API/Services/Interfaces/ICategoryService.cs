using PersonalFinance.Shared.DTOs.Categories;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync(Guid userId);
        Task<CategoryDto> CreateAsync(Guid userId,CreateCategoryRequestDto requestDto);
    }
}
