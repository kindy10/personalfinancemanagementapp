namespace PersonalFinance.Shared.DTOs.Reports;

public class MonthlyTrendDto
{
    public string Month { get; set; }

    public decimal Income { get; set; }

    public decimal Expense { get; set; }
}