using MyBudget.Core.Models.Account;
using System;

namespace MyBudget.Core.Models
{
    public class GoalModel
    {
        public int ID { get; set; }

        public string GoalName { get; set; }

        public byte Type { get; set; }

        public double CurrentAmount { get; set; }

        public double TotalAmount { get; set; }

        public bool IsActive { get; set; }

        public UserModel User { get; set; }

        public string UserID { get; set; }

        public DateTime? CompleteDate { get; set; }

        public string Description { get; set; }

        public CurrencyModel Currency { get; set; }

        public int CurrencyID { get; set; }
    }
}
