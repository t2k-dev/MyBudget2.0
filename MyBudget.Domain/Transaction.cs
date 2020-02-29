using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Domain
{
    public class Transaction
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public Category Category { get; set; }

        public int CategoryID { get; set; }

        public bool IsSpending { get; set; }

        public string Description { get; set; }

        public bool IsPlaned { get; set; }

        public User User { get; set; }

        public string UserID { get; set; }
    }
}
