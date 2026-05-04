using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Transactions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace PersonalFinance.API.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            //try
            //{
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null) return Unauthorized("Invalid token");
                var userId = Guid.Parse(userIdClaim.Value);
                var result = await _transactionService.GetAllAsync(userId);
                return Ok(result);
            /*}
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }*/
        }

        [HttpPost]
        public async Task <IActionResult> Create([FromBody] CreateTransactionRequestDto request)
        {
            //try
            //{
                var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var result = await _transactionService.CreateAsync(userId, request);
                return Ok(result);
            /*}
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }*/
        }
        [HttpDelete("{id}/{userId}")]
        public async Task<IActionResult> Delete(Guid id, Guid userId)
        {
            //try
            //{
                var success = await _transactionService.DeleteAsync(id, userId);
                if (!success) return NotFound();
                return Ok();
           /* }
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }*/
        }
    }
}
