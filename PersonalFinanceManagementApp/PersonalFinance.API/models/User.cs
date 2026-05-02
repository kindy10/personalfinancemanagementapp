using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.API.Models
{
    public class User
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public List<Category> Categories { get; set; } = new();
        public List<Budget> Budgets { get; set; } = new();
        public List<Transaction> Transactions { get; set; } = new();
        
        

    }
}
