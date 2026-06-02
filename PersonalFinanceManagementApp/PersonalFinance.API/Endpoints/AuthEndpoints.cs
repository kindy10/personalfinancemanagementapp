using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Auth;
using PersonalFinance.Shared.DTOs.Common;
using System.Security.Claims;

namespace PersonalFinance.API.Endpoints;

public static class AuthEndpoints
{
    public static void MapAuthEndpoints(
    this WebApplication app)
    {
        var group =
            app.MapGroup("/api/auth")
               .WithTags("Authentication");

        group.MapPost("/register",
            async (
                RegisterRequestDto request,
                IAuthService authService) =>
            {
                var result =
                    await authService.RegisterAsync(request);

                return Results.Ok(
                    ApiResponse<object>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        group.MapPost("/login",
            async (
                LoginRequestDto request,
                IAuthService authService) =>
            {
                var result =
                    await authService.LoginAsync(request);

                return Results.Ok(
                    ApiResponse<object>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        group.MapPost("/change-password",
            async (
                ClaimsPrincipal user,
                ChangePasswordRequestDto request,
                IAuthService authService) =>
            {
                var userId =
                    Guid.Parse(
                        user.FindFirst(
                            ClaimTypes.NameIdentifier)!
                            .Value);

                await authService.ChangePasswordAsync(
                    userId,
                    request);

                return Results.Ok(
                    new ApiResponse<string>
                    {
                        Success = true,
                        Data =
                            "Password changed successfully"
                    });
            })
            .RequireAuthorization();
    }
}