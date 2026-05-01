using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Shared.models
{
    public  class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public CategoryType Type { get; set; }
        public DateTime CreatedAt { get; set; }

        //Foreign Key
        public Guid  UserId { get; set; }
        //Navigation properties
        public User User { get; set; }

        //public Guid TransactionId { get; set; }
        public List<Transaction> Transactions { get; set; } = new();

        //public Guid BudgetId { get; set; }
        public List<Budget> Budgets { get; set; } = new();

    }
    public enum CategoryType
    {
        Income = 0,
        Expense = 1
    }
}
