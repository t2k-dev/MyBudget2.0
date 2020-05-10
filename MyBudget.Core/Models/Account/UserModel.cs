using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Models.Account
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public Guid ID { get; set; }
        public int DefaultCurrencyID { get; set; }
        public CurrencyModel DefaultCurrency { get; set; }
        public bool CarryoverRests { get; set; }
        public bool UseTemplates { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<UserCategoryModel> UserCategories { get; set; }
    }
}
