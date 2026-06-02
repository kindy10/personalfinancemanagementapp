using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Transactions;
using System.Security.Claims;

namespace PersonalFinance.API.Endpoints;

public static class TransactionEndpoints
{
    public static void MapTransactionEndpoints(
        this WebApplication app)
    {
        var group = app.MapGroup("/api/transactions")
            .WithTags("Transactions")
            .RequireAuthorization();  //For Authorize

        // GET ALL

        group.MapGet("/",
            async (
                ClaimsPrincipal user,
                ITransactionService transactionService) =>
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
                    await transactionService
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
                CreateTransactionRequestDto request,
                ITransactionService transactionService) =>
            {
                var userId =
                    Guid.Parse(
                        user.FindFirst(
                            ClaimTypes.NameIdentifier)!
                            .Value);

                var result =
                    await transactionService
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
                UpdateTransactionRequestDto request,
                ITransactionService transactionService) =>
            {
                var userId =
                    Guid.Parse(
                        user.FindFirst(
                            ClaimTypes.NameIdentifier)!
                            .Value);

                var result =
                    await transactionService
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
                ITransactionService transactionService) =>
            {
                var userId =
                    Guid.Parse(
                        user.FindFirst(
                            ClaimTypes.NameIdentifier)!
                            .Value);

                await transactionService
                    .DeleteAsync(
                        id,
                        userId);

                return Results.NoContent();
            });
    }
}