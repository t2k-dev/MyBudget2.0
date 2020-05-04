using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Transaction
{
    public class TransactionCreateRequestDTO
    {
        public double Amount { get; set; }

        public DateTime TransDate { get; set; }

        public bool IsSpending { get; set; }

        public string UserId { get; set; }

        public string Name { get; set; }

        public int CategoryId { get; set; }

        public bool IsPlaned { get; set; }

        public int CurrencyID { get; set; }
    }
}
