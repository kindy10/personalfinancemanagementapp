using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Reports
{
    public  class MonthlyReportDto
    {
        public decimal TotalIncome { get; set; }
        public decimal TotalExpenses { get; set; }

        public decimal Balance { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
    }
}
