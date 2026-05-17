using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Profile;
using System.Security.Claims;

namespace PersonalFinance.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly IProfileService _profileService;

    public ProfileController(
        IProfileService profileService)
    {
        _profileService = profileService;
    }

    // Get profile
    [HttpGet]
    public async Task<IActionResult> GetProfile()
    {
        var userId = Guid.Parse( User.FindFirst(ClaimTypes.NameIdentifier)?.Value);

        var result = await _profileService .GetProfileAsync(userId);

        return Ok(ApiResponse<UserProfileDto>.SuccessResponse( result,"Success"));
    }

    // Update profile
    [HttpPut]
    public async Task<IActionResult> UpdateProfile(
        [FromBody]
        UpdateProfileRequestDto request)
    {
        var userId = Guid.Parse(  User.FindFirst( ClaimTypes.NameIdentifier)?.Value);

        var result =
            await _profileService .UpdateProfileAsync(  userId, request);

        return Ok(ApiResponse<UserProfileDto>.SuccessResponse( result, "Profile updated"));
    }
}