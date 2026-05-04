using PersonalFinance.API.Models;
using PersonalFinance.Shared.DTOs.Categories;

namespace PersonalFinance.API.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryDto>> GetAllAsync(Guid userId);
        Task<CategoryDto> CreateAsync(Guid userId,CreateCategoryRequestDto requestDto);
        Task<CategoryDto> UpdateAsync(Guid id,Guid userId, UpdateCategoryRequestDto request);
        Task  DeleteAsync(Guid id, Guid userId);
    }
}
 