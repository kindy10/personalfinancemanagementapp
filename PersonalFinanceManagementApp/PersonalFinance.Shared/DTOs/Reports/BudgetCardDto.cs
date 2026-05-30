
using System.Drawing;


namespace PersonalFinance.Shared.DTOs.Reports
{
    public class BudgetCardDto
    {
        public Guid Id { get; set; }

        public string CategoryName { get; set; }

        public decimal Limit { get; set; }

        public decimal Spent { get; set; }

        public decimal Remaining { get; set; }

        public double Percentage { get; set; }

        public DateTime Month { get; set; }

        // UI Helpers

        public double ProgressValue => Percentage / 100.0;

        public string PercentageText => $"{Percentage:0}%";

        public string MonthText => Month.ToString("MMMM yyyy");

        public string Status =>
                Percentage switch
                {
                    <= 70 => "Safe",
                    <= 90 => "Warning",
                    _ => "Danger"
                };
    }
}
