using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Profile;
using System.Security.Claims;

namespace PersonalFinance.API.Endpoints;

public static class ProfileEndpoints
{
    public static void MapProfileEndpoints(
        this WebApplication app)
    {
        var group = app.MapGroup("/api/profile")
            .WithTags("Profile")
            .RequireAuthorization();

        // GET PROFILE

        group.MapGet("/",
            async (
                ClaimsPrincipal user,
                IProfileService profileService) =>
            {
                var userIdClaim =
                    user.FindFirst(
                        ClaimTypes.NameIdentifier);

                if (userIdClaim is null)
                    return Results.Unauthorized();

                var userId =
                    Guid.Parse(
                        userIdClaim.Value);

                var result =
                    await profileService
                        .GetProfileAsync(userId);

                return Results.Ok(
                    ApiResponse<UserProfileDto>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        // UPDATE PROFILE

        group.MapPut("/",
            async (
                ClaimsPrincipal user,
                UpdateProfileRequestDto request,
                IProfileService profileService) =>
            {
                var userIdClaim =
                    user.FindFirst(
                        ClaimTypes.NameIdentifier);

                if (userIdClaim is null)
                    return Results.Unauthorized();

                var userId =
                    Guid.Parse(
                        userIdClaim.Value);

                var result =
                    await profileService
                        .UpdateProfileAsync(
                            userId,
                            request);

                return Results.Ok(
                    ApiResponse<UserProfileDto>
                        .SuccessResponse(
                            result,
                            "Profile updated"));
            });
    }
}