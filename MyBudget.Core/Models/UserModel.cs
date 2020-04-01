using System;
using System.Collections.Generic;
using System.Text;

namespace MyBudget.Core.Models
{
    public class UserModel
    {
        public Guid ID { get; set; }
        public int DefaultCurrencyID { get; set; }
        public CurrencyModel DefaultCurrency { get; set; }
        public bool CarryoverRests { get; set; }
        public bool UseTemplates { get; set; }
        public DateTime? UpdateDate { get; set; }
        public List<UserCategoryModel> UserCategories { get; set; }
    }
}
