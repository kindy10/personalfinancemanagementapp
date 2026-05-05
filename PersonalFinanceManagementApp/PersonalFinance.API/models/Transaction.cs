using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.API.Models
{
    public  class Transaction
    {
        public Guid Id { get; set; }

        public decimal Amount { get; set; }
        public DateTime Date { get; set; } //business
        public DateTime CreatedAt { get; set; } //system

        public string Description { get; set; }

        //Foreign keys
        public Guid  CategoryId { get; set; }
        public Guid UserId { get; set; }

        //Navigation properties
        public Category Category { get; set; } 
        public User User { get; set; }
        
        
    }
}
