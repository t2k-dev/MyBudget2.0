using System;

namespace MyBudget.Core.Models.Account
{
    public class UserConfigs
    {
        public int DefaultCurrencyID { get; set; }
        public bool CarryoverRests { get; set; }
        public DateTime? UpdateDate { get; set; }
    }
}
