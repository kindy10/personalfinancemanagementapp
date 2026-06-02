using PersonalFinance.API.Services.Interfaces;
using PersonalFinance.Shared.DTOs.Common;
using PersonalFinance.Shared.DTOs.Reports;
using System.Security.Claims;

namespace PersonalFinance.API.Endpoints;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(
        this WebApplication app)
    {
        var group = app.MapGroup("/api/reports")
            .WithTags("Reports")
            .RequireAuthorization();

        // SUMMARY

        group.MapGet("/summary",
            async (
                ClaimsPrincipal user,
                IReportService reportService) =>
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
                    await reportService
                        .GetSummaryAsync(userId);

                return Results.Ok(
                    ApiResponse<SummaryDto>
                        .SuccessResponse(
                            result,
                            "Summary retrieved"));
            });

        // MONTHLY REPORT

        group.MapGet("/monthly",
            async (
                int month,
                int year,
                ClaimsPrincipal user,
                IReportService reportService) =>
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
                    await reportService
                        .GetMonthlyReportAsync(
                            userId,
                            month,
                            year);

                return Results.Ok(
                    ApiResponse<MonthlyReportDto>
                        .SuccessResponse(
                            result,
                            "Summary retrieved"));
            });

        // BUDGET USAGE

        group.MapGet("/budget-usage",
            async (
                ClaimsPrincipal user,
                IReportService reportService) =>
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
                    await reportService
                        .GetBudgetUsageAsync(userId);

                return Results.Ok(
                    ApiResponse<List<BudgetUsageDto>>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        // EXPENSE CATEGORIES

        group.MapGet("/expense-categories",
            async (
                ClaimsPrincipal user,
                IReportService reportService) =>
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
                    await reportService
                        .GetExpenseByCategoryAsync(
                            userId);

                return Results.Ok(
                    ApiResponse<List<ExpenseCategoryDto>>
                        .SuccessResponse(
                            result,
                            "Success"));
            });

        // MONTHLY TRENDS

        group.MapGet("/monthly-trends",
            async (
                ClaimsPrincipal user,
                IReportService reportService) =>
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
                    await reportService
                        .GetMonthlyTrendsAsync(
                            userId);

                return Results.Ok(
                    ApiResponse<List<MonthlyTrendDto>>
                        .SuccessResponse(
                            result,
                            "Success"));
            });
    }
}