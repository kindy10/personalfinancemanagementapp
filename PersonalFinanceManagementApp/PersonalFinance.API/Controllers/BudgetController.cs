using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Budgets;
using System.Security.Claims;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/budgets")]
    public class BudgetController :ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        
        [HttpGet]
        public async Task<IActionResult>GetAll()
        {
            //try
            //{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _budgetService.GetAllAsync(userId);
                return Ok(result);
            /*}
            catch (Exception ex) { 
                return BadRequest(new {message = ex.Message});
            }*/
        }


        //-----------------------------CREATE-------------------------------------//
        [HttpPost]
        public async Task<IActionResult> Create(CreateBudgetRequestDto request)
        {
            //try
            //{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _budgetService.CreateAsync(userId, request);
                return Ok(result);
            /*}
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }*/

        }

        //---------------------UPDATE------------------------------//
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, UpdateBudgetRequestDto request)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _budgetService.UpdateAsync(id, userId, request);

            return Ok(result);

        }

        //------------------------DELETE------------------------///
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _budgetService.DeleteAsync(id, userId);

            return NoContent();
        }
    }

}
