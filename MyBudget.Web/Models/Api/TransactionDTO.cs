using System;

namespace MyBudget.Web.Models.Api
{
    public class TransactionDTO
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public double Amount { get; set; }

        public DateTime TransactionDate { get; set; }

        public string CategoryName { get; set; }

        public bool IsSpending { get; set; }

        public bool IsPlaned { get; set; }
    }
}
