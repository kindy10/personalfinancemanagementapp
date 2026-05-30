using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.DTOs.Transactions
{
    public  class TransactionDto
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date {  get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Icon { get; set; } = "📊";

    }
}
