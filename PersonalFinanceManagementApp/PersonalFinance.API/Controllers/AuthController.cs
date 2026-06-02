////using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.Identity.Data;
//using Microsoft.AspNetCore.Mvc;
//using PersonalFinance.API.Services.Interfaces;
//using PersonalFinance.Shared.DTOs.Auth;
//using PersonalFinance.Shared.DTOs.Common;
//using System.Security.Claims;

//namespace PersonalFinance.API.Controllers
//{
//    [ApiController]
//    [Route("api/auth")]
//    public class AuthController :ControllerBase
//    {
//        private readonly IAuthService _authService;

//        public AuthController(IAuthService authService)
//        {
//            _authService = authService;
//        }

//        [HttpPost ("register")]
//        public async Task<IActionResult> Register([FromBody] RegisterRequestDto request)
//        {
           
//                var result = await _authService.RegisterAsync(request);
//                return Ok(ApiResponse<object>.SuccessResponse(result, "Success"));
            

//        }

//        [HttpPost("login")]
//        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
//        {
           
//                var result = await _authService.LoginAsync(request);
//                return Ok(ApiResponse<object>.SuccessResponse(result, "Success"));
            

//        }
//        [HttpPost("change-password")]
//        public async Task<IActionResult> ChangePassword( ChangePasswordRequestDto request)
//        {
//            var userId =
//                Guid.Parse(
//                    User.FindFirst(ClaimTypes.NameIdentifier)!
//                        .Value);

//            await _authService.ChangePasswordAsync(
//                userId,
//                request);

//            return Ok(
//                new ApiResponse<string>
//                {
//                    Success = true,
//                    Data = "Password changed successfully"
//                });
//        }

//    }
    
//}
