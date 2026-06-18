
namespace PersonalFinance.Shared.DTOs.Reports
{
    public class BudgetUsageDto
    {
        public string CategoryName { get; set; }
        public Guid CategoryId { get; set; }

        public decimal Limit { get; set; }

        public decimal Spent { get; set; }

        public decimal Remaining { get; set; }

        public double PercentageUsed { get; set; }
        public double DisplayPercentage => PercentageUsed * 100;
        public string RemainingText =>
            Remaining >= 0
        ? $"Remaining: {Remaining:C}"
        : $"Over Budget: {Math.Abs(Remaining):C}";
    }
}
