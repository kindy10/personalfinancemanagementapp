using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Categories;
using PersonalFinance.Shared.DTOs.Common;
using System.Security.Claims;

namespace PersonalFinance.API.Endpoints;

public static class CategoryEndpoints
{
    public static void MapCategoryEndpoints(
        this WebApplication app)
    {
        var group = app.MapGroup("/api/categories")
            .WithTags("Categories")
            .RequireAuthorization();

        // GET ALL

        group.MapGet("/",
            async (
                ClaimsPrincipal user,
                ICategoryService categoryService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                var result =
                    await categoryService
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
                CreateCategoryRequestDto request,
                ICategoryService categoryService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                var result =
                    await categoryService
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
                UpdateCategoryRequestDto request,
                ICategoryService categoryService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                var result =
                    await categoryService
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
                ICategoryService categoryService) =>
            {
                var userId = Guid.Parse(
                    user.FindFirst(
                        ClaimTypes.NameIdentifier)!
                        .Value);

                await categoryService
                    .DeleteAsync(
                        id,
                        userId);

                return Results.NoContent();
            });
    }
}