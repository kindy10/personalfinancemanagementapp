using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Budgets;
using PersonalFinance.Shared.DTOs.Common;
using System.Security.Claims;

namespace PersonalFinance.API.Endpoints;

public static class BudgetEndpoints
{
    public static void MapBudgetEndpoints(
        this WebApplication app)
    {
        var group = app.MapGroup("/api/budgets")
            .WithTags("Budgets")
            .RequireAuthorization();

        // GET ALL

        group.MapGet("/",
            async (
                ClaimsPrincipal user,
                IBudgetService budgetService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                var result =
                    await budgetService
                        .GetAllAsync(userId);

                return Results.Ok(
                    ApiResponse<object>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        // CREATE

        group.MapPost("/",
            async (
                ClaimsPrincipal user,
                CreateBudgetRequestDto request,
                IBudgetService budgetService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                var result =
                    await budgetService
                        .CreateAsync(
                            userId,
                            request);

                return Results.Ok(
                    ApiResponse<object>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        // UPDATE

        group.MapPut("/{id:guid}",
            async (
                Guid id,
                ClaimsPrincipal user,
                UpdateBudgetRequestDto request,
                IBudgetService budgetService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                var result =
                    await budgetService
                        .UpdateAsync(
                            id,
                            userId,
                            request);

                return Results.Ok(
                    ApiResponse<object>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        // DELETE

        group.MapDelete("/{id:guid}",
            async (
                Guid id,
                ClaimsPrincipal user,
                IBudgetService budgetService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                await budgetService
                    .DeleteAsync(
                        id,
                        userId);

                return Results.NoContent();
            });
    }
}