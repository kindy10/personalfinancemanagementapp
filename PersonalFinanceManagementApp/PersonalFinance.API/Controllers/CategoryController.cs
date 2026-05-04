using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Categories;
using System.Security.Claims;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController:ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
               _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //try
            //{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _categoryService.GetAllAsync(userId);
                return Ok(result);
            /*}
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }*/
        }

        //------------------------------------------------------------CREATE-------------------------------//
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryRequestDto request)
        {
            //try
            //{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _categoryService.CreateAsync(userId, request);
                return Ok(result);
           /* }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }*/

        }

        //----------------------------------------------UPDATE------------------------------------//
        [HttpPut("{id}")]
        public async Task <IActionResult> Update(Guid id, UpdateCategoryRequestDto  request)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _categoryService.UpdateAsync(id, userId, request);
            return Ok(result);


        }

        //----------------------------------DELETE----------------------------------------------//
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

             await _categoryService.DeleteAsync(id, userId);

            return NoContent();

        }
    }
}
