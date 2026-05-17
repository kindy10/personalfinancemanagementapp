using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Reports
{
    public class BudgetUsageDto
    {
        public string CategoryName { get; set; }

        public decimal Limit { get; set; }

        public decimal Spent { get; set; }

        public decimal Remaining { get; set; }

        public double PercentageUsed { get; set; }
    }
}
