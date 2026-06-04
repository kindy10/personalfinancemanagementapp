using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Reports
{
    public class TransactionCardDto
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public string CategoryName { get; set; }

        public string CategoryType { get; set; }

        public string Icon { get; set; } = "📊";

        // UI Helpers

        public string AmountText =>
            CategoryType == "Income"
                ? $"+{Amount:C}"
                : $"-{Amount:C}";

        public string AmountColor =>
            CategoryType == "Income"
                ? "#22C55E"
                : "#EF4444";

        public string DateText =>
            Date.ToString("dd MMM yyyy");
    }
}
