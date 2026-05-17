using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Budgets
{
    public  class UpdateBudgetRequestDto
    {
        public decimal MonthlyLimit { get; set; }
        public DateTime Month { get; set; }
        //public int Year { get; set; }
        public Guid CategoryId { get; set; }
    }
}
