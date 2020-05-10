using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyBudget.API.Models.Transaction
{
    public class TransactionListItem
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime TransDate { get; set; }

        public int CategoryId { get; set; }

        public bool IsSpending { get; set; }

        public string Description { get; set; }

        public bool IsPlaned { get; set; }

        public string UserId { get; set; }

        public string Currency { get; set; } //TODO check
    }
}
