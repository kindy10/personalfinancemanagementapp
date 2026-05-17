using BCrypt.Net;

using Microsoft.EntityFrameworkCore;

using PersonalFinance.API.Models;

using PersonalFinance.Shared.DTOs.Enums;

namespace PersonalFinance.API.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(
        AppDbContext context)
    {
        // Prevent duplicate seeding
        if (await context.Users.AnyAsync())
            return;

        // Create test user
        var user = new User
        {
            Id = Guid.NewGuid(),

            Name = "Mamadou Kindy",

            SurName = "Bah",

            Email = "bah@test.com",

            PasswordHash =
                BCrypt.Net.BCrypt.HashPassword(
                    "12345")
        };

        context.Users.Add(user);

        // Categories
        var foodCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Food",
            Type = CategoryType.Expense,
            UserId = user.Id
        };

        var transportCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Transport",
            Type = CategoryType.Expense,
            UserId = user.Id
        };

        var salaryCategory = new Category
        {
            Id = Guid.NewGuid(),
            Name = "Salary",
            Type = CategoryType.Income,
            UserId = user.Id
        };

        context.Categories.AddRange(
            foodCategory,
            transportCategory,
            salaryCategory);

        // Transactions
            var transactions = new List<Transaction>
            {
                new()
                {
                Id = Guid.NewGuid(),

                Amount = 3000,

                Description = "Monthly salary",

                Date = DateTime.Now.AddDays(-5),

                CreatedAt = DateTime.Now,

                CategoryId = salaryCategory.Id,

                UserId = user.Id
            },

            new()
            {
                Id = Guid.NewGuid(),

                Amount = 50,

                Description = "Restaurant",

                Date = DateTime.Now.AddDays(-2),

                CreatedAt = DateTime.Now,

                CategoryId = foodCategory.Id,

                UserId = user.Id
            },

            new()
            {
                Id = Guid.NewGuid(),

                Amount = 20,

                Description = "Taxi",

                Date = DateTime.Now.AddDays(-1),

                CreatedAt = DateTime.Now,

                CategoryId = transportCategory.Id,

                UserId = user.Id
            }
        };
        context.Transactions.AddRange(transactions);

        // Budgets
        var budgets = new List<Budget>
        {
            new()
            {
                Id = Guid.NewGuid(),

                MonthlyLimit = 500,

                Month = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    1),

                CategoryId = foodCategory.Id,

                UserId = user.Id
            },

            new()
            {
                Id = Guid.NewGuid(),

                MonthlyLimit = 200,

                Month = new DateTime(
                    DateTime.Now.Year,
                    DateTime.Now.Month,
                    1),

                CategoryId = transportCategory.Id,

                UserId = user.Id
            }
        };

        context.Budgets.AddRange(budgets);

        await context.SaveChangesAsync();
    }
}