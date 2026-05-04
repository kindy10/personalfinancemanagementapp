using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Categories;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
               _categoryService = categoryService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            //try
            //{
                var result = await _categoryService.GetAllAsync(userId);
                return Ok(result);
            /*}
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }*/
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Create(Guid userId, [FromBody] CreateCategoryRequestDto request)
        {
            //try
            //{
                var result = await _categoryService.CreateAsync(userId, request);
                return Ok(result);
           /* }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }*/

        }
    }
}
