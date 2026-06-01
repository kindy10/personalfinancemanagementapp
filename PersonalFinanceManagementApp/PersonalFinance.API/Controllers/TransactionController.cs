using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Transactions;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using PersonalFinance.Shared.DTOs.Common;

namespace PersonalFinance.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/transactions")]
    public class TransactionController : ControllerBase
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
            return Ok(ApiResponse<object>.SuccessResponse(result, "Success"));

            /*}
            catch(Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }*/
        }

        //---------------------------------------------------------CREATE--------------------------///
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTransactionRequestDto request)
        {
            //try
            //{
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _transactionService.CreateAsync(userId, request);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Success"));
            /*}
            catch(Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }*/
        }

        //---------------------------------------------------------UPDATE-----------------------------------//

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTransactionRequestDto request)
        {
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var result = await _transactionService.UpdateAsync(id, userId, request);
            return Ok(ApiResponse<object>.SuccessResponse(result, "Success"));
        }


        //--------------------------------------------DELETE----------------------------------//
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            //try
            //{
            var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            await _transactionService.DeleteAsync(id, userId);

            return NoContent();
            /* }
             catch(Exception ex)
             {
                 return BadRequest(new { message = ex.Message });
             }*/
        }

    }
}
