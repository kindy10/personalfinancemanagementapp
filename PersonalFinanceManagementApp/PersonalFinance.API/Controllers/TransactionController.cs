using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Transactions;

namespace PersonalFinance.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController:ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        //For now we pass userId manually

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetAll(Guid userId)
        {
            try
            {
                var result = await _transactionService.GetAllAsync(userId);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("{userId}")]
        public async Task <IActionResult> Create(Guid userId, [FromBody] CreateTransactionRequestDto request)
        {
            try
            {
                var result = await _transactionService.CreateAsync(userId, request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
        [HttpDelete("{id}/{userId}")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            try
            {
                var success = await _transactionService.DeleteAsync(id, userId);
                if (!success) return NotFound();
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
