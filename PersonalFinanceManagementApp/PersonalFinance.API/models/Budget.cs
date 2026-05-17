using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.API.Models
{
    public  class Budget
    {
        public Guid Id { get; set; }
        public decimal MonthlyLimit { get; set; }
        public DateTime CreatedAt { get; set ; }
        public DateTime  Month {  get; set; }
        //public int Year { get; set; }

        //Foreign keys
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        //Navigation properties
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
