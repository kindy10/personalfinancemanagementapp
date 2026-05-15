using PersonalFinance.Shared.DTOs.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinance.Mobile.Helpers
{
    public  class TemporaryData
    {
        //Stroes selected transaction temporarily
        public static TransactionDto selectedTransaction { get; set; }
    }
}
