using MyBudget.Core.Models.Account;
using System;

namespace MyBudget.Core.Models
{
    public class TransactionModel
    {
        public Guid ID { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public CategoryModel Category { get; set; }

        public int CategoryID { get; set; }

        public bool IsSpending { get; set; }

        public string Description { get; set; }

        public bool IsPlaned { get; set; }

        public UserModel User { get; set; }

        public string UserID { get; set; }

        public CurrencyModel Currency { get; set; }

        public int CurrencyID { get; set; }
    }
}
