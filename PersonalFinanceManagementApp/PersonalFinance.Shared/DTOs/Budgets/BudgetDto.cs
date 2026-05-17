using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Budgets
{
    public class BudgetDto
    {
        public Guid Id { get; set; }
        public decimal MonthlyLimit { get; set; }
        public DateTime Month { get; set; }
        //public int Year { get; set; }

        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}
