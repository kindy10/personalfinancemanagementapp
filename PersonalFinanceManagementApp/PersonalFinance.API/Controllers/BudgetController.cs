using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Budgets;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetController :ControllerBase
    {
        private readonly IBudgetService _budgetService;

        public BudgetController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult>GetAll(Guid userId)
        {
            try
            {
                var result = await _budgetService.GetAllAsync(userId);
                return Ok(result);
            }
            catch (Exception ex) { 
                return BadRequest(new {message = ex.Message});
            }
        }

        [HttpPost("{userId}")]
        public async Task<IActionResult> Create(Guid userId, [FromBody] CreateBudgetRequestDto request)
        {
            try
            {
                var result = await _budgetService.CreateAsync(userId, request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
    }
}
