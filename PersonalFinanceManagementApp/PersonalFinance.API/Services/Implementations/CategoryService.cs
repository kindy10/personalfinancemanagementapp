using Microsoft.EntityFrameworkCore;
using PersonalFinance.API.Data;
using PersonalFinance.API.Exceptions;
using PersonalFinance.API.Models;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Categories;

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
                    Type = c.Type
                })
                .ToList();
        }

        //-------------------------------------CREATE-----------------------------//
        public async Task<CategoryDto> CreateAsync(Guid userid, CreateCategoryRequestDto request)
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Type = request.Type,
                UserId = userid,
                CreatedAt = DateTime.UtcNow,

            };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type
            };
        }
        //----------------------------------UPDATE--------------------------------------------//
        public async Task<CategoryDto> UpdateAsync(Guid id, Guid userId, UpdateCategoryRequestDto request)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (category is null)
                throw  new AppException("Category not found");
            category.Name = request.Name;
            category.Type = request.Type;
            category.UserId = userId;
            category.CreatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Type = category.Type

            };

        }

        //-------------------------DELETE-------------------------------------------------//
        public async Task  DeleteAsync(Guid id, Guid userId)
        {
            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
            if (category is null) 
                throw new AppException("Category not found");

             _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
           
        }
    }
}
