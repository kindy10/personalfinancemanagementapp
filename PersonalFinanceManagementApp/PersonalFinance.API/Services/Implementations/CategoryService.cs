using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Exceptions;
using PersonalFinance.API.Models;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Categories;
using PersonalFinance.Shared.DTOs.Enums;

namespace PersonalFinance.API.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;

        public CategoryService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoryDto>> GetAllAsync(Guid userId)
        {
            return  _context.Categories
                .Where(c => c.UserId == userId)
                .Select (c => new CategoryDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Type = c.Type.ToString()
                })
                .ToList();
        }

        //-------------------------------------CREATE-----------------------------//
        public async Task<CategoryDto> CreateAsync(Guid userId, CreateCategoryRequestDto request)
        {

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new AppException("Category name is required");

            if (!Enum.IsDefined(typeof(CategoryType), request.Type))
                throw new AppException("Invalid category type");
            var existingCategory =await _context.Categories
                    .FirstOrDefaultAsync(c =>
                    c.UserId == userId &&
                    c.Name.ToLower() == request.Name.ToLower());

            if (existingCategory != null)
            {
                throw new AppException(
                    "Category already exists");
            }
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                Type = request.Type

            };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type.ToString()
            };
        }
        //----------------------------------UPDATE--------------------------------------------//
        public async Task<CategoryDto> UpdateAsync(Guid id, Guid userId, UpdateCategoryRequestDto request)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (category is null)
                throw  new AppException("Category not found");

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new AppException("Category name is required");

            if (!Enum.IsDefined(typeof(CategoryType), request.Type))
                throw new AppException("Invalid category type");

            category.Name = request.Name;
            category.UserId = userId;
            category.CreatedAt = DateTime.UtcNow;
            category.Type = request.Type;
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type.ToString()

            };

        }

        //-------------------------DELETE-------------------------------------------------//
        public async Task  DeleteAsync(Guid id, Guid userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (category is null) 
                throw new AppException("Category not found");

            var hasTransactions = await _context.Transactions
                    .AnyAsync(t => t.CategoryId == id);

            if (hasTransactions)
            {
                throw new AppException(
                    "Cannot delete category because transactions use it");
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
           
        }
    }
}
